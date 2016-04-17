using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Xml.Linq;

namespace AppSampleCWS
{
    class SampleForm : ContentPage
    {
        public SampleForm()
        {
            Entry cIdCliente = new Entry
            {
                Placeholder = "Id Cliente",
                Keyboard = Keyboard.Text
            };

            Button btnFindCliente = new Button
            {
                Text = "Find Cliente by ID",
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var labelInfo = new Label
            {
                Text = "Results",
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
            };

            var layoutFindIDCliente = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 10, 10, 20),
                Spacing = 10,

                Children = {
                    cIdCliente,
					btnFindCliente,
                    labelInfo
				}
            };

            Content = layoutFindIDCliente;

            btnFindCliente.Clicked += (sender, args) =>
            {
                
                //Set URl for Web service + method + parameter = value parameter:
                string sWebServiceURL = "http://financias.cloudapp.net/servidor.asmx/buscarClientePorIdCliente?codCliente=" +
                        cIdCliente.Text.Trim();
                //Var for get results:
                string cResultadoWS = "";
                //Run Web service/method and store results to object var:
                var webPage = new HttpClient().GetStringAsync(new Uri(sWebServiceURL));
                //Assign results in object var to string var:
                cResultadoWS = webPage.Result.ToString();
                //Extracts data (for example, Descricao):
                string cDescricao = "";
                string cTag = "<Descricao>";
                int startIndexForTag = cResultadoWS.IndexOf(cTag) + cTag.Length;
                int endIndexForTag = cResultadoWS.IndexOf("</Descricao>", startIndexForTag);

                cDescricao = cResultadoWS.Substring(startIndexForTag, endIndexForTag - startIndexForTag);

                //Set the label:
                labelInfo.Text = cDescricao;


            };
        }
    }
}
