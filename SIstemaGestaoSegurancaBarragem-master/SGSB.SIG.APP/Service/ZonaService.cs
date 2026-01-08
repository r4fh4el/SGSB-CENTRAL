using SGSB.SIG.APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGSB.SIG.APP.Service
{
    public class ZonaService
    {
       public static string URI = @"https://api.sgsb.com.br";
        // public static string URI = @"https://10.0.2.2:7042";
        //public static string URI = @"http://10.0.2.2:7042";
        HttpClient http;
        public ZonaService(HttpClient _http)
        {
            this.http = _http;
        }
        private static HttpClient CreateInsecureHttpClient()
        {
            // Crie um manipulador de HttpClient personalizado que aceita todos os certificados
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            // Configure o HttpClient para usar o handler personalizado
            return new HttpClient(handler);
        }
        public static async Task<bool> ExcluirZona(int id)
        {





            using (HttpClient cliente = CreateInsecureHttpClient())
            {
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                // HTTP POST - define o produto
                
                try
                {
                    // Serializa o objeto ZonaModel para JSON
                    try
                    {
                        HttpResponseMessage response = await cliente.PostAsJsonAsync(URI + "/API/ExcluirZona", id);

                        if (response.IsSuccessStatusCode)
                        {
                            // Processar a resposta bem-sucedida aqui
                            return true;
                        }
                        else
                        {
                            // Processar a resposta de erro aqui
                            string content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Erro: {response.StatusCode}, Conteúdo: {content}");

                        }
                    }
                    catch (Exception ex)
                    {
                        // Lidar com exceções aqui
                        Console.WriteLine($"Exceção: {ex.Message}");
                    }





                    // Processa a resposta
                    // ...
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro na solicitação HTTP: {ex}");
                }

                // Use PostAsync with the modified URL
                //HttpResponseMessage response = await cliente.PostAsync($"{URI}/API/AdicionarZona/{queryString}", null);


                //if (response.IsSuccessStatusCode)
                //{
                //    Uri produtoUrl = response.Headers.Location;

                //    return true;

                //}
                //else
                //{
                //    return false;
                //}
            }
            return false;
        }  
        
        public static async Task<bool> CadastrarZona(ZonaModel zonaModel)
        {




            using (HttpClient cliente = CreateInsecureHttpClient())
            {
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(URI + @"/API/ListarVertedouro/");

                // HTTP POST - define o produto
                //var aAthModel = new AuthModel()
                //{
                //    login = model.login,
                //    senha = model.senha,
                //};

                // Use PostAsync instead of PostAsJsonAsync
                //string queryString = $"?cpf={Uri.EscapeDataString(zonaModel.cpf == null ? "" : zonaModel.cpf.ToString())}&nomeCidadao={Uri.EscapeDataString(zonaModel.nomeCidadao == null ? "" : zonaModel.nomeCidadao.ToString())}" +
                //    $"&observacao={Uri.EscapeDataString(zonaModel.observacao == null ? "" : zonaModel.observacao.ToString())}" +
                //    $"&renda={Uri.EscapeDataString(zonaModel.renda == null ? "" : zonaModel.renda.ToString())}" +
                //    $"&dataNascimento={Uri.EscapeDataString(zonaModel.dataNascimento.ToString() )}" +
                //    $"&endereco={Uri.EscapeDataString(zonaModel.endereco == null ? "" : zonaModel.endereco.ToString())}" +
                //    $"&id={Uri.EscapeDataString(zonaModel.id.ToString())}" +
                //    $"&idZonaNome={Uri.EscapeDataString(zonaModel.idZonaNome == null ? "" : zonaModel.idZonaNome.ToString())}" +
                //    $"&numeroPessoas={Uri.EscapeDataString(zonaModel.numeroPessoas.ToString())}" +
                //    $"&numeroTelefone={Uri.EscapeDataString(zonaModel.numeroTelefone == null ? "" : zonaModel.numeroTelefone.ToString())}" +
                //    $"&area={Uri.EscapeDataString(zonaModel.area == null ? "" : zonaModel.area.ToString())}" +
                //    $"&usuario={Uri.EscapeDataString(zonaModel.usuario == null ? "" : zonaModel.usuario.ToString())}";

                // HTTP POST - define o produto
                var ZonaModel = new ZonaModel()
                {
                    nomeCidadao = zonaModel.nomeCidadao,
                    area = zonaModel.area,
                    dataNascimento = zonaModel.dataNascimento,
                    cpf = zonaModel.cpf,
                    endereco = zonaModel.endereco,
                    id = zonaModel.id,
                    idZonaNome = zonaModel.idZonaNome,
                    numeroPessoas = zonaModel.numeroPessoas,
                    numeroTelefone = zonaModel.numeroTelefone,
                    observacao = zonaModel.observacao,
                    renda = zonaModel.renda,
                    usuario = zonaModel.usuario



                };
                try
                {
                    // Serializa o objeto ZonaModel para JSON
                    try
                    {
                        HttpResponseMessage response = await cliente.PostAsJsonAsync(URI + "/API/AdicionarZona", ZonaModel);

                        if (response.IsSuccessStatusCode)
                        {
                            // Processar a resposta bem-sucedida aqui
                            return true;
                        }
                        else
                        {
                            // Processar a resposta de erro aqui
                            string content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Erro: {response.StatusCode}, Conteúdo: {content}");

                        }
                    }
                    catch (Exception ex)
                    {
                        // Lidar com exceções aqui
                        Console.WriteLine($"Exceção: {ex.Message}");
                    }





                    // Processa a resposta
                    // ...
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro na solicitação HTTP: {ex}");
                }

                // Use PostAsync with the modified URL
                //HttpResponseMessage response = await cliente.PostAsync($"{URI}/API/AdicionarZona/{queryString}", null);


                //if (response.IsSuccessStatusCode)
                //{
                //    Uri produtoUrl = response.Headers.Location;

                //    return true;

                //}
                //else
                //{
                //    return false;
                //}
            }
            return false;
        }


        public static async Task<ObservableCollection<ZonaModel>> GetBuscarListZona()
        {
            ObservableCollection<ZonaModel> lstRotaFuga = new ObservableCollection<ZonaModel>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var httpClient = CreateInsecureHttpClient();
                    ////MUDAR O LOCALHOST SEU ANIMAL
                    //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    lstRotaFuga = await httpClient.GetFromJsonAsync<ObservableCollection<ZonaModel>>(@"https://api.sgsb.com.br/API/ListarZona/");
                }
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"InvalidCastException: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                // Add more details as needed
                throw; // Rethrow the exception to ensure it's not silently ignored
            }
            return lstRotaFuga;
        }


    }
}
