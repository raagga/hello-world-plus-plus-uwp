using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // greetingOutput.Text = "Hello, " + nameInput.Text + "!";
            var parameters = new ExchangeParams();
            parameters.param1 = "Hello! ";
            parameters.param2 = nameInput.Text;

            //await SendPostRequest(parameters, ServerUrl);

            JsonObject obj = await GetAsync(ServerUrl);
            System.Diagnostics.Debug.WriteLine(obj);


            // Frame.Navigate(typeof(PageOne), parameters);
        }

        const string ServerUrl = "http://petstore.swagger.io/v2/pet/3"; //specify your server url

        public void ClientHeaderInfo(HttpClient client)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task SendPostRequest(ExchangeParams param, string url)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    await client.PostAsJsonAsync(url, param);

                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async Task<JsonObject> GetAsync(string uri)
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(uri);
            return await Task.Run(() => JsonObject.Parse(content));
        }

    }
}
