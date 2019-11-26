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
using SQLite;
using System.IO;

namespace Miramar_Health
{
    [Activity(Label = "PerfilActivity")]
    public class PerfilActivity : Activity
    {
        static BancoSqLite bancoLocal;
        public static BancoSqLite BancoLocal
        {
            get;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_perfil);
            Button btnVoltar = (Button)FindViewById(Resource.Id.btn_voltar_perfil);
            Button btnDesconectar = (Button)FindViewById(Resource.Id.btn_desconectar_perfil);
            ImageButton imgbtnAlterar = (ImageButton)FindViewById(Resource.Id.imgbtn_alterar_perfil);


            // Create your application here
           
        }
    }
}