using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HTTPProtocol
{
    class Program
    {
        private const string reqToken = "94443a47320658cf448f615cebc82125ef16cee85784694d587d81a0c3ce34e7";
        private static HttpClient _client;

        static async Task Main(string[] args)
        {
            CreateClient();

            var optiune = 2;

            while (optiune != 1)
            {
                AfisareMesaj();

                Console.WriteLine("\nAlegeti optiune ");
                optiune = int.Parse(Console.ReadLine());

                switch (optiune)
                {
                    case 1: return;
                    case 2: await PostAsync(); break;
                    case 3: await GetSingleAsync(); break;
                    case 4: await DeleteAsync(); break;
                    case 5: await GetAllAsync(); break;
                    case 6: await HeadAsync(); break;
                    case 7: await OptionsAsync(); break;
                }
            }
        }

        private static void AfisareMesaj()
        {
            Console.WriteLine("1) Iesire");
            Console.WriteLine("2) Adaugare persoana noua");
            Console.WriteLine("3) Get persoana by id");
            Console.WriteLine("4) Sterge persoana persoana by id");
            Console.WriteLine("5) Get person lists");
            Console.WriteLine("6) Head request");
            Console.WriteLine("7) Options request");
        }

        private static void CreateClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reqToken);
        }

        private static async Task ShowRequestDetails(HttpResponseMessage httpResponseMessage)
        {
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine($"Mesaj returnat  {await httpResponseMessage.Content.ReadAsStringAsync()}");
            Console.WriteLine($"IsSuccessStatusCode {httpResponseMessage.IsSuccessStatusCode}");
            Console.WriteLine($"StatusCode {httpResponseMessage.StatusCode}");
            Console.WriteLine("------------------------------------------------------------------------");
        }

        public static async Task GetAllAsync()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("https://gorest.co.in/public-api/users");

            await ShowRequestDetails(httpResponse);
        }

        public static async Task PostAsync()
        {
            var url = "https://gorest.co.in/public-api/users";

            Console.WriteLine("Dati nume");
            string nume = Console.ReadLine();
            Console.WriteLine("Dati email");
            string email = Console.ReadLine();
            Console.WriteLine("Dati gen(Male/Female)");
            string gen = Console.ReadLine();
            Console.WriteLine("Dati status(Active/Inactive)");
            string status = Console.ReadLine();

            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync(url,
                new
                {
                    name=nume,
                    email=email,
                    gender = gen,
                    status = status
                });

            await ShowRequestDetails(httpResponse);
        }

        public static async Task GetSingleAsync()
        {
            Console.WriteLine("Dati Id: ");
            var id = Console.ReadLine();
            HttpResponseMessage httpResponse = await _client.GetAsync("https://gorest.co.in/public-api/users/"+id);

            await ShowRequestDetails(httpResponse);
        }

        public static async Task DeleteAsync()
        {
            Console.WriteLine("Dati id");
            var id = Console.ReadLine();
            var url = "https://gorest.co.in/public-api/users/"+id;

            HttpResponseMessage httpResponse = await _client.DeleteAsync(url);

            await ShowRequestDetails(httpResponse);
        }

        public static async Task HeadAsync()
        {
            var url = "https://gorest.co.in/public-api/users";

            HttpResponseMessage httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

            await ShowRequestDetails(httpResponse);
        }

        public static async Task OptionsAsync()
        {
            var url = "https://gorest.co.in/public-api/users";

            HttpResponseMessage httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Options, url));

            await ShowRequestDetails(httpResponse);
        }

    }
}
