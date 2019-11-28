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
    [Activity(Label = "IniciarAtendimentoActivity")]
    public class IniciarAtendimentoActivity : Activity
    {
        Paciente Pac;
        List<Paciente> ListPac;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_iniciar_atendimento);
            Spinner spnPaciente = (Spinner)FindViewById(Resource.Id.spinner2);
            EditText edtRegiaoFerida = (EditText)FindViewById(Resource.Id.edt_regiaoferida_atendimento);
            EditText edtDescricao = (EditText)FindViewById(Resource.Id.edt_descricaoferida_iniciar);
            EditText edtCobertura = (EditText)FindViewById(Resource.Id.edt_cobertura_atendimento);

            // Create your application here


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
    }
}