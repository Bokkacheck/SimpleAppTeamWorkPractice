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
            client.BaseAddress = new Uri("http://192.168.1.11:5000");
            HttpResponseMessage responseMessage = client.GetAsync("api/getPeople").Result;
            var people = responseMessage.Content.ReadAsStringAsync().Result;
            var peoples = JsonConvert.DeserializeObject<List<People>>(people,podesavanjeJson);
            foreach(var p in peoples)
            {
                listBox1.Items.Add(p.firstName + " " + p.lastName);
            }
        }
    }
}
