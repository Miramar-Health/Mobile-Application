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
    [Activity(Label = "BuscarPacienteActivity")]
    public class BuscarPacienteActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_buscar_paciente);
            EditText edtBuscarNome = (EditText)FindViewById(Resource.Id.edt_nomebuscar_buscar);
            EditText edtNomePaciente = (EditText)FindViewById(Resource.Id.edt_nomebuscar_buscar);
            EditText edtRegiaoFerida = (EditText)FindViewById(Resource.Id.edt_regiaoferida_buscar);
            // Create your application here
        }
    }
}