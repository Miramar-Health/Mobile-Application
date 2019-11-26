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
    [Activity(Label = "RecuperarSenhaActivity")]
    public class RecuperarSenhaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_recuperar_senha);
            Button btnVoltar = (Button)FindViewById(Resource.Id.btn_voltar_recuperarsenha);

            // Create your application here

            btnVoltar.Click += delegate
            {
                SetContentView(Resource.Layout.activity_main);
            };
        }
    }
}