using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace ClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public JsonSerializerSettings podesavanjeJson = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
        private void btnPrikaz_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");
            HttpResponseMessage responseMessage = client.GetAsync("api/getPeople").Result;
            var people = responseMessage.Content.ReadAsStringAsync().Result;
            var peoples = JsonConvert.DeserializeObject<List<People>>(people,podesavanjeJson);
            foreach(var p in peoples)
            {
                listBox1.Items.Add(p.firstName + " " + p.lastName);
            }
        }

        private void Button1_ClickAsync(object sender, EventArgs e)
        {
            PostRequest("http://localhost:5000/newPerson");
        }

        private async void PostRequest(string url)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("id","3"),
                new KeyValuePair<string, string>("firstName","Milovan"),
                new KeyValuePair<string, string>("lastName","Srejic"),
                new KeyValuePair<string, string>("age","21")
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;

                        MessageBox.Show(mycontent);
                    }
                }
            }
        }
    }
}
