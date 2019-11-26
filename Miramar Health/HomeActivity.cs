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


namespace Miramar_Health
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_home);

            // Create your application here
            Button btnIniciarAtendimento = (Button)FindViewById(Resource.Id.btn_iniciaratendimento_home);
            Button btnCadastrarPaciente = (Button)FindViewById(Resource.Id.btn_cadastrarpaciente_home);
            Button btnBuscarPaciente = (Button)FindViewById(Resource.Id.btn_buscarpaciente_home);
            ImageButton imgbtnPerfil = (ImageButton)FindViewById(Resource.Id.imageButton1);

            btnIniciarAtendimento.Click += delegate
            {
                StartActivity(typeof(IniciarAtendimentoActivity));
            };

            btnCadastrarPaciente.Click += delegate
            {
                StartActivity(typeof(CadastrarPacienteActivity));
            };

            btnBuscarPaciente.Click += delegate
            {
                StartActivity(typeof(BuscarPacienteActivity));
            };

            imgbtnPerfil.Click += delegate
            {
                StartActivity(typeof(PerfilActivity));
            };



        }
    }
}