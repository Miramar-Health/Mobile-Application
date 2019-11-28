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

namespace Miramar_Health
{
    [Activity(Label = "RegistrarActivity")]
    public class RegistrarActivity : Activity
    {
        Categoria Cat;
        Tecnico tec;
        List<Categoria> ListCat;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_registrar);
            EditText edtNome = (EditText)FindViewById(Resource.Id.edt_nome_registrar);
            EditText edtEmail = (EditText)FindViewById(Resource.Id.edt_email_registrar);
            EditText edtCelular = (EditText)FindViewById(Resource.Id.edt_telefone_registrar);
            Spinner spnCategoria = (Spinner)FindViewById(Resource.Id.spn_categoria_registrar);
            EditText edtCoren = (EditText)FindViewById(Resource.Id.edt_coren_registrar);
            EditText edtCpf = (EditText)FindViewById(Resource.Id.edt_cpf_registrar);
            EditText edtSenha = (EditText)FindViewById(Resource.Id.edt_senha_registrar);
            EditText edtConfimarSenha = (EditText)FindViewById(Resource.Id.edt_confirmarsenha_registrar);
            Button btnContinuar = (Button)FindViewById(Resource.Id.btn_continuar_registrar);

            btnContinuar.Click += delegate
            {
                tec = new Tecnico();
                if (edtSenha.Text.Length < 6)
                {
                    Toast.MakeText(this, "Sua senha deve conter mais de 6 digitos", ToastLength.Long).Show();
                }
                else
                {
                    if (edtSenha.Text == edtConfimarSenha.Text)
                    {


                    
                        Toast.MakeText(this, "Inserido", ToastLength.Short).Show();
                        StartActivity(typeof(MainActivity));
                        
                    }
                    else
                    {
                        Toast.MakeText(this, "Algo de errado não esta certo", ToastLength.Short).Show();
                    }
                }
            };

            



            // Create your application here
            //inserindo categorias dentro do spinner
            Cat = new Categoria();
            ListCat = new List<Categoria>();
            ListCat = Cat.GerarLista();
            List<string> lCat = new List<string>();
            foreach (var item in ListCat)
            {
                lCat.Add(item.Categoriaa);
            }
            ArrayAdapter adapterCat = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, lCat);
            spnCategoria.Adapter = adapterCat;
            spnCategoria.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_itemselecionado_Categoria);
            
        }
        public void Spinner_itemselecionado_Categoria(object sender, AdapterView.ItemSelectedEventArgs lCat)
        {
            Cat = new Categoria();
            Toast.MakeText(this, "Categoria : " + ListCat[lCat.Position].Categoriaa.ToString(), ToastLength.Long).Show();
            Cat.Id_Categoria = ListCat[lCat.Position].Id_Categoria;
            Cat.Categoriaa = ListCat[lCat.Position].Categoriaa;
        }
        
                
    }
}