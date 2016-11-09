using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DemoMetup
{

    public partial class MenuPage : MasterDetailPage
    {
        public string cc;

        public MenuPage()
        {
            InitializeComponent();
            leer(true, true);


            
            telefono1.Keyboard = Keyboard.Numeric;
        }

        private async void leer(Object sender, object EventArgs)
        {
            MobileServiceClient client;
            IMobileServiceTable<AgendaTabla> tabla;
            client = new MobileServiceClient(Conexion.conexion);
            tabla = client.GetTable<AgendaTabla>();
            IEnumerable<AgendaTabla> items = await tabla.ToEnumerableAsync();
            string[] arreglo = new string[items.Count()];
            string[] arreglo2 = new string[items.Count()];
            string[] arreglo3 = new string[items.Count()];
            int i = 0;
            foreach (var x in items)
            {
                arreglo[i] = x.Name;
                arreglo2[i] = x.Lastname;
                arreglo3[i] = x.Cellphone;
                i++;

            }
            lista.ItemsSource = arreglo;


        }
        public async void busqueda(Object sender, EventArgs e)
        {
            MobileServiceClient client;
            IMobileServiceTable<AgendaTabla> tabla;
            client = new MobileServiceClient(Conexion.conexion);
            tabla = client.GetTable<AgendaTabla>();
            try
            {
                IEnumerable<AgendaTabla> items = await tabla.ToEnumerableAsync();
                string[] arreglo = new string[items.Count()];
                string[] arreglo2 = new string[items.Count()];
                string[] arreglo3 = new string[items.Count()];
                int i = 0;
                foreach (var x in items)
                {
                    arreglo[i] = x.Name;
                    arreglo2[i] = x.Lastname;
                    arreglo3[i] = x.Cellphone;
                    i++;


                    if (x.Name == buscar.Text)
                    {

                        nombre1.Text = x.Name;
                        apellido1.Text = x.Lastname;
                        telefono1.Text = x.Cellphone;
                    }
                    if (x.Cellphone == buscar.Text)
                    {

                        nombre1.Text = x.Name;
                        apellido1.Text = x.Lastname;
                        telefono1.Text = x.Cellphone;
                    }

                }


                lista.ItemsSource = from nombre in arreglo
                                    where nombre.StartsWith("" + buscar.Text)
                                    select nombre;

                






            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "no existe el contacto ", "OK");
                buscar.Text = "";

            }


        }
        private void selecciona(object sender, SelectedItemChangedEventArgs e)
        {
            buscar.Text = "" + e.SelectedItem;

        }


        //eliminar.Clicked += async (sender, args) =>

        public async void inserta(object sender, object args)
        {
            if (nombre1.Text == null || telefono1.Text == null || apellido1.Text == null)
            {
                await DisplayAlert("Alerta", "Te falta llenar algun campo para poder insertar el contacto ", "OK");
            }

            else
            {
                try
                {


                    MobileServiceClient client;
                    IMobileServiceTable<AgendaTabla> tabla;
                    client = new MobileServiceClient(Conexion.conexion);
                    tabla = client.GetTable<AgendaTabla>();

                    var datos = new AgendaTabla { Name = nombre1.Text, Lastname = apellido1.Text, Cellphone = telefono1.Text };

                    IEnumerable<AgendaTabla> items = await tabla.ToEnumerableAsync();
                    string[] arreglo = new string[items.Count()];
                    string[] arreglo2 = new string[items.Count()];

                    int i = 0;
                    foreach (var x in items)
                    {
                        arreglo[i] = x.Name;
                        arreglo2[i] = x.Lastname;
                        if (datos.Id == null)
                        {
                            await tabla.InsertAsync(datos);
                            await DisplayAlert("Alerta", "Tu contacto ha sido guardado con exito ", "OK");
                            nombre1.Text = null;
                            telefono1.Text = null;
                            apellido1.Text = null;
                            buscar.Text = null;
                        }


                        // lista2.ItemsSource = arreglo2;
                    }
                    leer(true, true);
                }
                catch (Exception e)
                {
                    await DisplayAlert("Alerta", "Te falta llenar algun campo para poder insertar\n el contacto ", "OK");

                }
            }

        }
        //guaradar fin

        public async void actualiza(object sender, object args)
        {
            if (nombre1.Text == null || telefono1.Text == null || apellido1.Text == null)
            {
                await DisplayAlert("Alerta", "Te falta llenar algun campo para poder actualizar el contacto ", "OK");
            }

            else
            {
                try { 

                MobileServiceClient client;
                IMobileServiceTable<AgendaTabla> tabla;
                client = new MobileServiceClient(Conexion.conexion);
                tabla = client.GetTable<AgendaTabla>();

                var datos = new AgendaTabla { Name = nombre1.Text, Lastname = apellido1.Text, Cellphone = telefono1.Text };

                IEnumerable<AgendaTabla> items = await tabla.ToEnumerableAsync();
                string[] arreglo = new string[items.Count()];
                string[] arreglo2 = new string[items.Count()];
                string[] arreglo3 = new string[items.Count()];
                string[] arreglo4 = new string[items.Count()];

                int i = 0;
                foreach (var x in items)
                {
                    arreglo[i] = x.Name;
                    arreglo2[i] = x.Lastname;
                    arreglo3[i] = x.Id;
                    arreglo4[i] = x.Cellphone;

                    if (x.Cellphone == telefono1.Text)
                    {
                        if (x.Name != nombre1.Text)
                        {
                            x.Name = nombre1.Text;
                        }
                        if (x.Lastname != apellido1.Text)
                        {
                            x.Lastname = apellido1.Text;
                        }

                        await tabla.UpdateAsync(x);
                        await DisplayAlert("Alerta", "Tu contacto ha sido actualizado con exito ", "OK");
                        nombre1.Text = null;
                        telefono1.Text = null;
                        apellido1.Text = null;
                            buscar.Text = null;

                        }
                    leer(true, true);
                }

                lista.ItemsSource = arreglo;
                // lista2.ItemsSource = arreglo2;

            }
                catch (Exception e)
            {
                await DisplayAlert("Alerta", "El contacto que estas intentando actualizar no existe  ", "OK");
            }
        }
        }
        //actualizar fin

         public async void  borrar(object sender, object args)
        {
            if (nombre1.Text == null || telefono1.Text == null || apellido1.Text == null)
            {
                await DisplayAlert("Alerta", "Te falta llenar algun campo para poder eliminar el contacto ", "OK");
            }

            else
            {
                try
                {
                    MobileServiceClient client;
                    IMobileServiceTable<AgendaTabla> tabla;
                    client = new MobileServiceClient(Conexion.conexion);
                    tabla = client.GetTable<AgendaTabla>();

                    var datos = new AgendaTabla { Name = nombre1.Text, Lastname = apellido1.Text, Cellphone = telefono1.Text };

                    IEnumerable<AgendaTabla> items = await tabla.ToEnumerableAsync();
                    string[] arreglo = new string[items.Count()];
                    string[] arreglo2 = new string[items.Count()];
                    string[] arreglo3 = new string[items.Count()];
                    string[] arreglo4 = new string[items.Count()];

                    int i = 0;
                    foreach (var x in items)
                    {
                        arreglo[i] = x.Name;
                        arreglo2[i] = x.Lastname;
                        arreglo3[i] = x.Id;
                        arreglo4[i] = x.Cellphone;

                        if (x.Cellphone == telefono1.Text)
                        {
                            if (x.Name != nombre1.Text)
                            {
                                x.Name = nombre1.Text;
                            }
                            if (x.Lastname != apellido1.Text)
                            {
                                x.Lastname = apellido1.Text;
                            }

                            await tabla.DeleteAsync(x);
                            nombre1.Text = null;
                            telefono1.Text = null;
                            apellido1.Text = null;
                            buscar.Text = null;
                            await DisplayAlert("Alerta", "El contacto ha sido eliminado con exito  ", "OK");
                        }
                        leer(true, true);
                    }
                    lista.ItemsSource = arreglo;
                }
                catch (Exception e) {
                    await DisplayAlert("Alerta", "El contacto que estas intentando eliminar no existe  ", "OK");
                }

               
                // lista2.ItemsSource = arreglo2;
            }
        }
        //borrar fin

        private void limpiar(object sender, object args)
           {
               nombre1.Text = null;
               telefono1.Text = null;
               apellido1.Text = null;
           }
    }
}