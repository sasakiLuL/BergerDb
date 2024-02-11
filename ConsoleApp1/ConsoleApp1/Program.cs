using Microsoft.VisualBasic.FileIO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

using var reader = new TextFieldParser("data.csv");

reader.CommentTokens = ["#"];
reader.SetDelimiters([","]);
reader.HasFieldsEnclosedInQuotes = true;

reader.ReadLine();

var customers = new List<Customer>();

while (!reader.EndOfData)
{
    var values = reader.ReadFields();

    customers.Add(new Customer()
    {
        institution = values[0],
        memberType = (int)Enum.Parse(typeof(MemberType), values[1]),
        paymentType = (int)Enum.Parse(typeof(PaymentType), values[2]),
        sex = (int)Enum.Parse(typeof(Sex), values[3]),
        prefix = values[4],
        firstName = values[5],
        lastName = values[6],
        email = values[7],
        street = values[8],
        zipCode = values[9],
        city = values[10],
        amount = Decimal.Parse(values[11]),
        entryType = (int)Enum.Parse(typeof(EntryType), values[12]),
        personalId = long.Parse(values[13]),
        registrationDate = DateTime.Parse(values[14]).ToUniversalTime(),
        terminatedOn = values[15].Length != 0 ? DateTime.Parse(values[15]).ToUniversalTime() : null,
        lastInvoiceSendedOn = values[16].Length != 0 ? DateTime.Parse(values[16]).ToUniversalTime() : null,
        lastCreditReceivedOn = values[17].Length != 0 ? DateTime.Parse(values[17]).ToUniversalTime() : null,
        notation = values[18] + "\n" + values[19],
    });
}

string fileName = "customers.json";
await using FileStream createStream = File.Create(fileName);
await JsonSerializer.SerializeAsync(createStream, customers, new JsonSerializerOptions { WriteIndented = true });

HttpClient _client = new HttpClient();

_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzaWQiOiJiNGM0YjU2Zi1iMDBmLTQ4NzktYTZiZS04YzFmYjg2M2IzMzQiLCJlbWFpbCI6InZiQHZiLmRlIiwibmFtZSI6IlZlcmVuYSIsInBlcm1pc3Npb25zIjpbIlVzZXJzQ3JlYXRlIiwiVXNlcnNSZWFkIiwiVXNlcnNVcGRhdGUiLCJVc2Vyc0RlbGV0ZSIsIkN1c3RvbWVyc0NyZWF0ZSIsIkN1c3RvbWVyc1JlYWQiLCJDdXN0b21lcnNVcGRhdGUiLCJDdXN0b21lcnNEZWxldGUiXSwiZXhwIjoxNzA3NjExMDI4LCJpc3MiOiJCZXJnZXJEQiIsImF1ZCI6IkJlcmdlckRCLmNvbSJ9.lMRpV_SS0chP-h3yPwUoArpBEIuQs4EenyQDPbpkvGA");

int i = 1;

foreach (var customer in customers)
{
    using StringContent jsonContent = new(JsonSerializer.Serialize(customer, new JsonSerializerOptions { WriteIndented = true }), new MediaTypeHeaderValue("application/json"));

    Console.WriteLine(JsonSerializer.Serialize(customer, new JsonSerializerOptions { WriteIndented = true }));

    using HttpResponseMessage response = await _client.PostAsync(
        "https://localhost:7152/api/customers",
        jsonContent);

    Console.Write(i + "\t");
    WriteRequestToConsole(response.EnsureSuccessStatusCode());

    var jsonResponse = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"{jsonResponse}\n");
    i++;
}

return 0;


static void WriteRequestToConsole(HttpResponseMessage response)
{
    if (response is null)
    {
        return;
    }

    var request = response.RequestMessage;
    Console.Write($"{request?.Method} ");
    Console.Write($"{request?.RequestUri} ");
    Console.WriteLine($"HTTP/{request?.Version}");
}

class Customer
{
    public string prefix { get; set; } = "";
    public string firstName { get; set; } = "";
    public string lastName { get; set; } = "";
    public string email { get; set; } = "";
    public long personalId { get; set; }
    public string notation { get; set; } = "";
    public int sex { get; set; }
    public DateTime registrationDate { get; set; }
    public string street { get; set; } = "";
    public string zipCode { get; set; } = "";
    public string city { get; set; } = "";
    public int memberType { get; set; }
    public string institution { get; set; } = "";
    public int entryType { get; set; }
    public int paymentType { get; set; }
    public decimal amount { get; set; }
    public DateTime? currentInvoiceSendedOn { get; set; } = null;
    public DateTime? lastInvoiceSendedOn { get; set; } = null;
    public DateTime? currentCreditReceivedOn { get; set; } = null;
    public DateTime? lastCreditReceivedOn { get; set; } = null;
    public DateTime? terminatedOn { get; set; } = null;
}

public enum EntryType
{
    GE,
    EE
}

public enum MemberType
{
    Apo,
    Laie,
    Arzt,
    Heilpraktiker = 3,
    Heilpraktikerin = 3
}

public enum PaymentType
{
    Rechnung,
    Einzug,
}

public enum Sex
{
    Herr,
    Frau,
}