using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekat2TVP
{
    public partial class FormaZaKasira : Form
    {

        OleDbDataReader reader;
        OleDbConnection conn;
        OleDbCommand cmd;
        List<Artikal> listaArtikla = new List<Artikal>();
        List<Artikal> listaArtiklaRacun = new List<Artikal>();
        List<Racun> listaRacun = new List<Racun>();
       
        public FormaZaKasira()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //popunjavanje liste artiklima
        private void PopuniListBoxArtiklima(string upit)
        {
            listBoxDostupniProizvodi.Items.Clear();
            conn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Hype\Desktop\Sve\Projekat Cs\Projekat2TVP\Projekat2TVP\bin\Debug\BazaZaKasu.accdb");
            cmd = new OleDbCommand(upit, conn);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string naziv = ((string)reader["Naziv"]);
                    double cena = ((double)(reader["Cena"]));
                    int popust = ((int)reader["Popust"]);
                    int komada = 1;
                    Artikal a = new Artikal(naziv, cena, popust, komada);
                    bool f = true;
                    foreach (Artikal k in listaArtikla)
                    {
                        if (k.Naziv.Equals(a.Naziv))
                            f = false;
                    }
                    if (f == true)
                        listaArtikla.Add(a);

                    listBoxDostupniProizvodi.Items.Add(a.ZaListBoxTrenutni());
                    for (int i = 0; i < listBoxDostupniProizvodi.Items.Count; i++) {
                        listBoxDostupniProizvodi.SetSelected(0, true);
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                textBoxUnesiKolicinu.Text = "1";
                conn.Close();
            }
        }

        private void buttonAlkoholnaPica_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Alkoholna pica\")";
            PopuniListBoxArtiklima(upit);
        }
        private void buttonMeso_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Meso\")";
            PopuniListBoxArtiklima(upit);
        }
        private void buttonBezalkoholnaPica_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Bezalkoholna Pica\")";
            PopuniListBoxArtiklima(upit);
        }
        private void buttonMlecniProizvodi_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Mlecni proizvodi\")";
            PopuniListBoxArtiklima(upit);

        }
        private void buttonVoce_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Voce\")";
            PopuniListBoxArtiklima(upit);
        }
        private void buttonPovrce_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Povrce\")";
            PopuniListBoxArtiklima(upit);
        }
        private void buttonHemikalije_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Hemikalije\")";
            PopuniListBoxArtiklima(upit);
        }
        private void buttonOstalo_Click(object sender, EventArgs e)
        {
            string upit = "Select * from Artikli where ID_Grupa like(Select ID_Grupa from Grupe where NazivGrupe like \"Ostalo\")";
            PopuniListBoxArtiklima(upit);
        }
        //kraj popunjavanja liste artiklima



        private void FormaZaKasira_Load(object sender, EventArgs e)
        {
            tabControlDodajNoviArtikal.SelectedTab = tabPageKasir;
            textBoxUnesiKolicinu.Text = "1";
            comboBoxKategorija.Items.Add(buttonAlkoholnaPica.Text);
            comboBoxKategorija.Items.Add(buttonBezalkoholnaPica.Text);
            comboBoxKategorija.Items.Add(buttonOstalo.Text);
            comboBoxKategorija.Items.Add(buttonMeso.Text);
            comboBoxKategorija.Items.Add(buttonVoce.Text);
            comboBoxKategorija.Items.Add(buttonPovrce.Text);
            comboBoxKategorija.Items.Add(buttonHemikalije.Text);
            comboBoxKategorija.Items.Add(buttonMlecniProizvodi.Text);
            comboBoxKategorija.SelectedIndex = 0;
          

        }
        //dugmici za + i - komada proizvoda
        private void buttonPlusProizvod_Click(object sender, EventArgs e)
        {
            if (!listBoxDostupniProizvodi.SelectedIndex.Equals(-1))
            {
                int broj = Int32.Parse(textBoxUnesiKolicinu.Text);
                textBoxUnesiKolicinu.Text = (broj + 1).ToString();
            }
        }
        private void buttonMinusProizvod_Click(object sender, EventArgs e)
        {
            if (!listBoxDostupniProizvodi.SelectedIndex.Equals(-1))
            {
                int broj = Int32.Parse(textBoxUnesiKolicinu.Text);
                if (!(broj <= 1))
                    textBoxUnesiKolicinu.Text = (broj - 1).ToString();
            }
        }
        //kraj dugmica za komade
        private void listBoxDostupniProizvodi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!listBoxDostupniProizvodi.SelectedIndex.Equals(-1))
            {
                textBoxUnesiKolicinu.Text = "1";
            }


        }
        //Dodaj u korpu
        private void buttonDodajUKorpu_Click(object sender, EventArgs e)
        {
            if (!listBoxDostupniProizvodi.SelectedIndex.Equals(-1))
            {
                bool Ima = false;
                double suma = 0;
                foreach (Artikal k in listaArtikla)
                {
                    if (listBoxDostupniProizvodi.SelectedItem.ToString().Equals(k.ZaListBoxTrenutni()))
                    {
                        foreach (Artikal Z in listaArtiklaRacun)
                        {
                            if ((listBoxDostupniProizvodi.SelectedItem.ToString().Equals(Z.ZaListBoxTrenutni())))
                            {
                                Z.Komada = Z.Komada + Int32.Parse(textBoxUnesiKolicinu.Text);
                                listBoxRacun.Items.Clear();
                                Ima = true;
                            }
                        }
                        if (Ima == true)
                        {
                            foreach (Artikal g in listaArtiklaRacun)
                                listBoxRacun.Items.Add(g.ZaListBoxRacun());
                        }
                        else
                        {
                            k.Komada = k.Komada + Int32.Parse(textBoxUnesiKolicinu.Text) - 1;
                            listaArtiklaRacun.Add(k);
                            listBoxRacun.Items.Add(k.ZaListBoxRacun());
                        }
                    }
                }

                foreach (Artikal h in listaArtiklaRacun)
                    suma += (h.Komada * h.Cena);

                textBoxUkupanRacun.Text = suma.ToString();
            }
            else { MessageBox.Show("Niste odabrali nijedan proizvod!"); }
        }
        private void tabPageKasir_Click(object sender, EventArgs e)
        {

        }
        //Storniraj racun
        private void buttonStornirajRacun_Click(object sender, EventArgs e)
        {
            if (listBoxRacun.Items.Count > 0)
            {
                listBoxRacun.Items.Clear();
                foreach (Artikal k in listaArtiklaRacun)
                    k.Komada = 1;
                listaArtiklaRacun.Clear();
                textBoxUkupanRacun.Text = "0";
                MessageBox.Show("Uspesno ste stornirali ceo racun!");
            }
            else MessageBox.Show("Racun je prazan!");
        }
        //Ukloni sa racuna
        private void buttonUkloniSaRacuna_Click(object sender, EventArgs e)
        {
            if (!listBoxRacun.SelectedIndex.Equals(-1))
            {
                List<Artikal> NovaLista = new List<Artikal>();
                foreach (Artikal k in listaArtiklaRacun)
                    NovaLista.Add(k);
                foreach (Artikal k in listaArtiklaRacun)
                    if (k.ZaListBoxRacun().Equals(listBoxRacun.SelectedItem.ToString()))
                    { k.Komada = 1;
                        NovaLista.Remove(k);
                    }
                listBoxRacun.Items.Clear();
                listaArtiklaRacun.Clear();
                foreach (Artikal k in NovaLista)
                    listaArtiklaRacun.Add(k);
                double s = 0;
                foreach (Artikal k in listaArtiklaRacun)
                {
                    listBoxRacun.Items.Add(k.ZaListBoxRacun());
                    s += (k.Cena * k.Komada);

                }
                textBoxUkupanRacun.Text = s.ToString();

                if (listBoxRacun.Items.Count == 0) {
                    textBoxUkupanRacun.Text = "0";
                }
            }
            else
            {
                MessageBox.Show("Niste obelezili nijedan proizvod sa racuna!");
            }
        }
        //DODAJ ATIKAL
        public bool isNumberCena()
        {
            bool check = false, f = false;
            foreach (char k in textBoxCena.Text)
            {

                if (!char.IsDigit(k))
                {
                    check = true;
                }

            }
            if (check == true)
            {
                MessageBox.Show("Cena moze biti samo broj!"); f = true;
            }
            return f;
        }
        public bool isNumberPopust()
        {
            bool check = false, f = false;
            foreach (char k in textBoxPopust.Text)
            {

                if (!char.IsDigit(k))
                {
                    check = true;
                }

            }
            if (check == true)
            {
                MessageBox.Show("Popust moze biti samo broj!"); f = true;
            }
            int broj = 0;
            if (check == false && !textBoxPopust.Text.Equals(""))
            {
                broj = Int32.Parse(textBoxPopust.Text);
                if (broj > 99 || broj < 0)
                {
                    f = true;
                    MessageBox.Show("Popust moze biti od 1 do 99%");
                }
            }
            return f;
        }
        private bool IsInBase()
        {

            bool f = false;
            conn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Hype\Desktop\Sve\Projekat Cs\Projekat2TVP\Projekat2TVP\bin\Debug\BazaZaKasu.accdb");
            cmd = new OleDbCommand("select Naziv from artikli", conn);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                { if (((string)reader["naziv"]).ToLower().Equals(textBoxNaziv.Text.Trim().ToLower()))
                        f = true; }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { conn.Close(); }
            return f;
        }
        private void buttonDodajNoviArtikal_Click(object sender, EventArgs e)
        {

            if (textBoxPopust.Text.Equals("") || textBoxCena.Text.Equals("") || textBoxNaziv.Text.Equals(""))
            {
                MessageBox.Show("popunite sva polja pravilno!");
            }
            else if (isNumberCena().Equals(false) && isNumberPopust().Equals(false))
            {
                if (IsInBase())
                {
                    MessageBox.Show("Vec imamo taj proizvod!");
                    return;
                }
                else
                { int broj = 1;
                    if (comboBoxKategorija.SelectedItem.ToString().Equals("Alkoholno pice"))
                        broj = 1;
                    else if (comboBoxKategorija.SelectedItem.ToString().Equals("Bezalkoholna pica"))
                        broj = 2;
                    else if (comboBoxKategorija.SelectedItem.ToString().Equals("Meso"))
                        broj = 3;
                    else if (comboBoxKategorija.SelectedItem.ToString().Equals("Mlecni proizvodi"))
                        broj = 4;
                    else if (comboBoxKategorija.SelectedItem.ToString().Equals("Voce"))
                        broj = 5;
                    else if (comboBoxKategorija.SelectedItem.ToString().Equals("Povrce"))
                        broj = 6;
                    else if (comboBoxKategorija.SelectedItem.ToString().Equals("Hemikalije"))
                        broj = 7;
                    else if (comboBoxKategorija.SelectedItem.ToString().Equals("Ostalo"))
                        broj = 8;
                    conn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Hype\Desktop\Sve\Projekat Cs\Projekat2TVP\Projekat2TVP\bin\Debug\BazaZaKasu.accdb");
                    try
                    {
                        conn.Open();
                        cmd = new OleDbCommand("INSERT INTO Artikli (Naziv, Cena, Popust, Id_Grupa) VALUES (@Naziv, @Cena, @Popust, @Id_Grupa)", conn);
                        cmd.Parameters.AddWithValue("@Name", textBoxNaziv.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cena", textBoxCena.Text.Trim());
                        cmd.Parameters.AddWithValue("@Popust", textBoxPopust.Text.Trim());
                        cmd.Parameters.AddWithValue("@Id_Grupa", broj);
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    finally
                    {
                        conn.Close();
                        MessageBox.Show("Uspesno ste dodali artikal: " + Environment.NewLine + "Ime : " + textBoxNaziv.Text
                           + Environment.NewLine + " Cena: " + textBoxCena.Text + Environment.NewLine + "Popust: " + textBoxPopust.Text +
                           Environment.NewLine + "Kategorija : " + comboBoxKategorija.SelectedItem.ToString());
                        textBoxCena.Text = "";
                        textBoxNaziv.Text = "";
                        textBoxPopust.Text = "";
                    }
                }
            }
        }
        //KRAJ DODAJ ARTIKAL

        //IZDAJ RACUN
        private void buttonNaplati_Click(object sender, EventArgs e)
        {
            DateTime datum = DateTime.Now;
            DateTime vreme = DateTime.Now;
            if (!(listBoxRacun.Items.Count == 0))
            {
                Racun racun = new Racun(Double.Parse(textBoxUkupanRacun.Text), datum, vreme);

                conn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Hype\Desktop\Sve\Projekat Cs\Projekat2TVP\Projekat2TVP\bin\Debug\BazaZaKasu.accdb");

                try
                {
                    conn.Open();
                    cmd = new OleDbCommand("INSERT INTO Racuni ( Cena, Datum, Vreme) VALUES (@Cena, @Datum, @Vreme)", conn);
                    cmd.Parameters.AddWithValue("@Cena", racun.Cena);
                    cmd.Parameters.AddWithValue("@Datum", racun.Datum.ToShortDateString());
                    cmd.Parameters.AddWithValue("@Vreme", racun.Vreme.ToShortTimeString());
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                finally
                {
                    conn.Close();
                    MessageBox.Show("Uspesno ste prodali proizvode: " + Environment.NewLine + "Cena : " + textBoxUkupanRacun.Text + "   din. " +
                     Environment.NewLine + " Datum: " + racun.Datum.ToShortDateString() + Environment.NewLine + "Vreme : " + racun.Vreme.ToShortTimeString());


                }
            }
            else
                MessageBox.Show("Racun je prazan!");
        }

        //KRAJ IZDAVANJA RACUNA

        //Forma za prikaz racuna
        private void buttonPrikaziRacune_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listaRacun.Clear();
            conn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Hype\Desktop\Sve\Projekat Cs\Projekat2TVP\Projekat2TVP\bin\Debug\BazaZaKasu.accdb");
            cmd = new OleDbCommand("Select * from Racuni ", conn);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime datum = ((DateTime)reader["Datum"]);
                    double cena = ((double)(reader["Cena"]));
                    DateTime vreme = ((DateTime)reader["Vreme"]);
                    Racun a = new Racun(cena, datum, vreme);
                    bool f = true;
                    listaRacun.Add(a);
                    //listBox1.Items.Add(a.ToString());
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                conn.Close();
            }
            DateTime datumOd = dateTimePickerDatumOd.Value;
            DateTime datumDo = dateTimePickerDatumDo.Value;
            
            if ((DateTime.Compare(dateTimePickerDatumOd.Value, dateTimePickerDatumDo.Value) <= 0)
                || dateTimePickerDatumOd.Value.Day.ToString().Equals(dateTimePickerDatumDo.Value.Day.ToString()))
            {
                listBox1.Items.Clear();
                foreach (Racun k in listaRacun)
                {
                    if ((DateTime.Compare(k.Datum, datumOd) >= 0) && (DateTime.Compare(k.Datum, datumDo) <= 0) || k.Datum.Day.ToString().Equals(dateTimePickerDatumOd.Value.Day.ToString()))
                        listBox1.Items.Add(k.ToString());
                }
            
            } else
            {
                MessageBox.Show("Niste pravilno uneli datum!");
                listBox1.Items.Clear();
            }
        }

        private void tabPageprethodniRacuni_Click(object sender, EventArgs e)
        {
        }

        //Kraj forme za prikaz racuna


    }
}
