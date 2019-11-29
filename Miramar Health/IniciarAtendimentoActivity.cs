using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Miramar_Health.Classes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;

namespace Miramar_Health
{
    [Activity(Label = "IniciarAtendimentoActivity")]
    public class IniciarAtendimentoActivity : Activity
    {
        Paciente Pac;
        TipodeFerida TiFe;
        List<Paciente> ListPac;
        List<TipodeFerida> ListTiFe;
        ImageView ImageView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_iniciar_atendimento);
            Spinner spnPaciente = (Spinner)FindViewById(Resource.Id.spinner2);
            Spinner spnTipoFerida = (Spinner)FindViewById(Resource.Id.spn_tipodaferida_atendimento);
            EditText edtRegiaoFerida = (EditText)FindViewById(Resource.Id.edt_regiaoferida_atendimento);
            EditText edtDescricao = (EditText)FindViewById(Resource.Id.edt_descricaoferida_iniciar);
            EditText edtCobertura = (EditText)FindViewById(Resource.Id.edt_cobertura_atendimento);
            Button btnAbrirCamera = (Button)FindViewById(Resource.Id.btn_tirarfoto_atendimento);


            // Create your application here
            btnAbrirCamera.Click += BtnAbrirCamera_Click;
            
            

            // Inserindo Pacientes dentro do spinner
            Pac = new Paciente();
            ListPac = new List<Paciente>();
            ListPac = Pac.GerarLista();
            List<string> lPac = new List<string>();
            foreach (var item in ListPac)
            {
                lPac.Add(item.Nome);
            }
            
            ArrayAdapter adapterPac = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, lPac);
            spnPaciente.Adapter = adapterPac;
            spnPaciente.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_itemselecionado_paciente);
            spnPaciente.Prompt = "Nome do Paciente";

            // Inserindo Tipo de Ferida dentro do spinner
            TiFe = new TipodeFerida();
            ListTiFe = new List<TipodeFerida>();
            ListTiFe = TiFe.GerarLista();
            List<string> lTiFe = new List<string>();
            foreach (var item in ListTiFe)
            {
                lTiFe.Add(item.Nome_Ferida);
            }

            ArrayAdapter adapterTiFe = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, lTiFe);
            spnTipoFerida.Adapter = adapterTiFe;
            spnTipoFerida.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_selecao_tipoferida);
            spnTipoFerida.Prompt = "Tipo de Ferida";

        }
        public void Spinner_itemselecionado_paciente(object sender, AdapterView.ItemSelectedEventArgs lPac)
        {
            Pac = new Paciente();
            Toast.MakeText(this, "Paciente :" + ListPac[lPac.Position].Nome.ToString(), ToastLength.Long).Show();
            Pac.Id_paciente = ListPac[lPac.Position].Id_paciente;
            Pac.Nome = ListPac[lPac.Position].Nome;
            Pac.Local_ferida = ListPac[lPac.Position].Local_ferida;
            Pac.Descricao_inicial_ferida = ListPac[lPac.Position].Descricao_inicial_ferida;
            Pac.Data_cadastro = ListPac[lPac.Position].Data_cadastro;

        }

        public void Spinner_selecao_tipoferida(object sender, AdapterView.ItemClickEventArgs lTiFe)
        {
            TiFe = new TipodeFerida();
            Toast.MakeText(this, "Paciente :" + ListTiFe[lTiFe.Position].Nome_Ferida.ToString(), ToastLength.Long).Show();
            TiFe.Id_Ferida = ListTiFe[lTiFe.Position].Id_Ferida;
            TiFe.Nome_Ferida = ListTiFe[lTiFe.Position].Nome_Ferida;
        }

        //evento botão abrir camera

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            ImageView = new ImageView(this);
            ImageView.SetImageBitmap(bitmap);


        }

        private void BtnAbrirCamera_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

    }
}