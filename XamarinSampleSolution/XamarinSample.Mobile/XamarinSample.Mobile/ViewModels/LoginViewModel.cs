using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSample.Mobile.Pages;

namespace XamarinSample.Mobile.ViewModels
{
    sealed class LoginViewModel : ViewModelBase
    {
        private string _Usuario;
        public string Usuario
        {
            get => this._Usuario;
            set
            {
                this._Usuario = value;
                OnPropertyChanged();
            }
        }

        private string _Senha;
        public string Senha
        {
            get => this._Senha;
            set
            {
                this._Senha = value;
                OnPropertyChanged();
            }
        }

        private Command _LoginCommand;
        public Command LoginCommand => this._LoginCommand ??
            (this._LoginCommand = new Command(
                async () => await LoginCommandExecute()));

        private async Task LoginCommandExecute()
        {
            using (var _client = new HttpClient())
            {
                var _url = "http://ENDERECO_API/Token";

                var _params = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username",  this._Usuario),
                    new KeyValuePair<string, string>("password",  this._Senha)
                };

                var _content = new FormUrlEncodedContent(_params);

                using (var _response = await _client.PostAsync(_url, _content))
                {
                    if (_response.IsSuccessStatusCode)
                    {
                        var _responseModel = JsonConvert.DeserializeObject<LoginResponseModel>(
                            await _response.Content.ReadAsStringAsync());

                        await App.Current.MainPage
                            .DisplayAlert("Parabéns", $"Seu token é {_responseModel.access_token}", "Ok");

                        App.Current.MainPage = new InicioPage();
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Ooops", "Algo de errado não deu certo", "Ok");
                }
            }
        }

        private Command _RegisterCommand;
        public Command RegisterCommand => this._RegisterCommand ??
            (this._RegisterCommand = new Command(() => RegisterCommandExecute()));

        private void RegisterCommandExecute()
        {
            App.Current.MainPage = new RegistroPage();
        }
    }


    public class LoginResponseModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        public string issued { get; set; }
        public string expires { get; set; }
    }

}

