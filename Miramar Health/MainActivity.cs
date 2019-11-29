using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Reflection.Emit;
using System;
using SQLite;
using Miramar_Health.Classes;
using System.IO;
using Plugin.Media;
using Android.Content.PM;
using Plugin.Permissions;

namespace Miramar_Health
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AppCompatActivity
    {


        static BancoSqLite bancoLocal;
        public static BancoSqLite BancoLocal
        {
            get
            {
                if (bancoLocal == null)
                {
                    bancoLocal = new BancoSqLite(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "BancoSqLite.enfermagemdb"));
                }
                return bancoLocal;
            }
        }
        Tecnico tec;
        protected override void OnCreate(Bundle savedInstanceState)
        {




            SessaoTecnico st;
            try
            {
                var a = BancoLocal.ObterListaSessao();
                if (a.Result.Count != 0)
                {
                    foreach (var i in a.Result)
                    {
                        if (i.Sessao == true)
                        {
                            st = new SessaoTecnico();
                            StartActivity(typeof(HomeActivity));
                            Finish();
                            st.Id = i.Id;
                            Toast.MakeText(this, "Técnico logado com sucesso \n Nome: " + i.Tecnico, ToastLength.Long).Show();
                        }
                    }
                }
                else
                {
                    st = new SessaoTecnico
                    {
                        Sessao = false
                        
                    };
                    BancoLocal.InserirSessao(st);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            
          
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Button btnEntrar = (Button)FindViewById(Resource.Id.btn_entrar_main);
            EditText edtEmail = (EditText)FindViewById(Resource.Id.edt_email_main);
            EditText edtSenha = (EditText)FindViewById(Resource.Id.edt_senha_main);
            TextView textEsqueceu = (TextView)FindViewById(Resource.Id.textesqueceu_main);
            TextView textNaotem = (TextView)FindViewById(Resource.Id.textView10);
            btnEntrar.Click += delegate
            {
                tec = new Tecnico();
                if (tec.LogarTecnico(edtEmail.Text, edtSenha.Text))
                {
                    if (tec.Situacao == true)
                    {
                        st = new SessaoTecnico();
                        Toast.MakeText(this, "Tecnico : " + tec.Nome + " Logado com sucesso ", ToastLength.Long).Show();
                        StartActivity(typeof(HomeActivity));
                        st.Id = tec.Id_Tecnico;
                        st.Sessao = true;
                        st.Tecnico = tec.Nome;
                        BancoLocal.SalvarSessaoTecnico(st);
                        Finish();

                    }
                    else
                    {
                        Toast.MakeText(this, "Entre em contato com o seu Enfermeiro(a) responsável ", ToastLength.Long).Show();
                    }
                    
                }
                else
                {
                    Toast.MakeText(this, "Email ou Senha Incorreto !!  \n Tente Novamente !", ToastLength.Long).Show();
                }

                
            };
            textEsqueceu.Click += delegate
            {
                StartActivity(typeof(RecuperarSenhaActivity));
            };

            textNaotem.Click += delegate
            {
                StartActivity(typeof(RegistrarActivity));
            };

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }





    }
}