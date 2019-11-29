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
using System.IO;
using Android.Media;
using Android;

namespace Miramar_Health
{
    [Activity(Label = "IniciarAtendimentoActivity")]
    public class IniciarAtendimentoActivity : Activity
    {
        //----------------------------Novas instancias e variaveis -----------------------------------------------
        Paciente Pac;
        TipodeFerida TiFe;
        List<Paciente> ListPac;
        List<TipodeFerida> ListTiFe;
        ImageView ImageVie;
        int paciente;
        int ferida;
        RegistroAtendimento ini;
        static BancoSqLite bancoLocal;
        int tecnico;
        byte[] imageArray;

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };





        //-------------------------- Banco Sqlite ---------------------------------------------------------------------
        public static BancoSqLite BancoLocal
        {
            get
            {
                if (bancoLocal == null)
                {
                    bancoLocal = new BancoSqLite(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "BancoSqLite.enfermagemdb"));
                }
                return bancoLocal;
            }
        }


        //------------------------ Criando Métodos e Objetos ----------------------------------------------------------------------
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
                            tecnico = st.Id;
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
            SetContentView(Resource.Layout.activity_iniciar_atendimento);
            Spinner spnPaciente = (Spinner)FindViewById(Resource.Id.spinner2);
            Spinner spnTipoFerida = (Spinner)FindViewById(Resource.Id.spn_tipodaferida_atendimento);
            EditText edtRegiaoFerida = (EditText)FindViewById(Resource.Id.edt_regiaoferida_atendimento);
            EditText edtDescricao = (EditText)FindViewById(Resource.Id.edt_descricaoferida_iniciar);
            EditText edtCobertura = (EditText)FindViewById(Resource.Id.edt_cobertura_atendimento);
            Button btnAbrirCamera = (Button)FindViewById(Resource.Id.btn_tirarfoto_atendimento);
            Button btnFinalizar = (Button)FindViewById(Resource.Id.btn_finalizar_atendimento);


            // Create your application here
            // Envento de Abrir Camera
            btnAbrirCamera.Click += BtnAbrirCamera_Click;
            RequestPermissions(permissionGroup, 0);


            //Salvar Atendimento
            btnFinalizar.Click += delegate
            {
                ini = new RegistroAtendimento();
                if (edtRegiaoFerida.Text != string.Empty && edtDescricao.Text != string.Empty && edtCobertura.Text != string.Empty)
                {
                    ini = new RegistroAtendimento();
                    ini.InserirRegistroAtendimento(imageArray, edtDescricao.Text, DateTime.Now, edtCobertura.Text, tecnico, paciente, ferida);
                    if (ini.Id_atendimento > 0)
                    {
                        Toast.MakeText(this, "Atendimento Realizado com sucesso", ToastLength.Short).Show();
                        OnDestroy();
                        StartActivity(typeof(HomeActivity));
                    }
                    else
                    {
                        Toast.MakeText(this, "Erro ao finalizar atendimento", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Preencha todos os campos", ToastLength.Short).Show();
                }
            };

            

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
            spnTipoFerida.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Ferida);
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
            paciente = ListPac[lPac.Position].Id_paciente;
        }

        
        public void Ferida(object sender, AdapterView.ItemSelectedEventArgs lTiFe)
        {
            TiFe = new TipodeFerida();
            TiFe.Id_Ferida = ListTiFe[lTiFe.Position].Id_Ferida;
            TiFe.Nome_Ferida = ListTiFe[lTiFe.Position].Nome_Ferida;
            ferida = ListTiFe[lTiFe.Position].Id_Ferida;

        }

        //evento botão abrir camera

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            ImageVie = new ImageView(this);
            ImageVie.SetImageBitmap(bitmap);
      
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        private void BtnAbrirCamera_Click(object sender, EventArgs e)
        {
            TakePhoto();
        }

        async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
                Name = "mimage.jpg",
                Directory = "sample"

            });

            if (file == null)
            {
                return; 
            }

            //Convertendo o arquivo para byte[] 
            imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            
        }

        public byte[] imageToByteArray(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

    }
}