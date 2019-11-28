using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Miramar_Health.Classes;
using System.IO;

namespace Miramar_Health
{
    [Activity(Label = "ListarPacientesActivity")]
    public class CadastrarPacienteActivity : Activity
    {
        Paciente Pac;
        static BancoSqLite bancoLocal;
        int qualquer;
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
                            st.Id = i.Id;
                            qualquer = st.Id;
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
            SetContentView(Resource.Layout.activity_cadastrar_paciente);
            EditText edtNomePaciente = (EditText)FindViewById(Resource.Id.edt_nome_cadastrarpaciente);
            EditText edtLocalFerida = (EditText)FindViewById(Resource.Id.edt_localferida_cadastrarpaciente);
            EditText edtDescricao = (EditText)FindViewById(Resource.Id.editText3);
            Button btnCadastrar = (Button)FindViewById(Resource.Id.btn_cadastrar_paciente);
            Button btnVoltar = (Button)FindViewById(Resource.Id.btn_voltar_paciente);
            // Create your application here
            btnVoltar.Click += delegate
            {
                StartActivity(typeof(HomeActivity));
            };
            btnCadastrar.Click += delegate
            {
                
                Pac = new Paciente();
                if (edtNomePaciente.Text != string.Empty && edtLocalFerida.Text != string.Empty && edtDescricao.Text != string.Empty)
                {
                    Pac.InserirPaciente(edtNomePaciente.Text, edtLocalFerida.Text, edtDescricao.Text, DateTime.Now, qualquer);
                    if (Pac.Id_paciente > 0)
                    {
                        Toast.MakeText(this, "Paciente inserido com sucesso", ToastLength.Long).Show();
                        edtNomePaciente.Text = string.Empty;
                        edtLocalFerida.Text = string.Empty;
                        edtDescricao.Text = string.Empty;
                    }
                    else
                    {
                        Toast.MakeText(this, "Não cadastrado", ToastLength.Long).Show();
                        edtNomePaciente.Text = string.Empty;
                        edtLocalFerida.Text = string.Empty;
                        edtDescricao.Text = string.Empty;
                    }
                }
                else
                {
                    Toast.MakeText(this, "Todos os campos devem estar preenchidos", ToastLength.Long).Show();
                    edtNomePaciente.Text = string.Empty;
                    edtLocalFerida.Text = string.Empty;
                    edtDescricao.Text = string.Empty;
                }
            };
        }
    }
}