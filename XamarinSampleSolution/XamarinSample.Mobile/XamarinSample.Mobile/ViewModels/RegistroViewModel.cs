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
    class RegistroViewModel : ViewModelBase
    {
        private string _Usuario;
        public string Usuario
        {
            get => this._Usuario;
            set
            {
                this._Usuario = value;
                OnPropertyChanged();
                this.RegistrarCommand.ChangeCanExecute();
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
                this.RegistrarCommand.ChangeCanExecute();
            }
        }

        private string _SenhaConfirmar;
        public string SenhaConfirmar
        {
            get => this._SenhaConfirmar;
            set
            {
                this._SenhaConfirmar = value;
                OnPropertyChanged();
                this.RegistrarCommand.ChangeCanExecute();
            }
        }

        private Command _RegistrarCommand;
        public Command RegistrarCommand => this._RegistrarCommand ??
            (this._RegistrarCommand = new Command(
                async () => await RegistrarCommandExecute()
                , () => RegistrarCommandCanExecute()));

        private bool RegistrarCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(this._Usuario) &&
                !string.IsNullOrWhiteSpace(this._Senha) &&
                !string.IsNullOrWhiteSpace(this._SenhaConfirmar);
        }

        private async Task RegistrarCommandExecute()
        {
            using (var _client = new HttpClient())
            {
                var _url = "http://ENDERECO_API/api/Account/Register";

                var _model = new RegisterModel
                {
                    ConfirmPassword = this._SenhaConfirmar,
                    Email = this._Usuario,
                    Password = this._Senha,
                };

                var _content = new StringContent(JsonConvert.SerializeObject(_model)
                    , Encoding.UTF8, "application/json");

                using (var _response = await _client.PostAsync(_url, _content))
                {
                    if (_response.IsSuccessStatusCode)
                    {
                        await App.Current.MainPage.DisplayAlert("Parabéns", "Agora você já pode logar na sua aplicação", "Ok");

                        App.Current.MainPage = new LoginPage();
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Ooops", "Algo de errado não deu certo", "Ok");
                }
            }


        }
    }


    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}

