using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VitaminList
{
    class Element
    {
        public string name;
        public int avarage;
        public Element(string _name, int _avarage)
        {
            name = _name;
            avarage = _avarage;
        }

    }
    internal class VitaminsData
    {
        private Panel MainLayout;
        public int avarage;
        private int AvarageOfTree;
        Element[] sorted_elements;
        Heap<Element> heap;

        int[] eded;
        public int number_of_elements = 27;
        Dictionary<int, Element> Deffults = new Dictionary<int, Element>
        {
            { 0, new Element("Ретинол",900) },
            { 1, new Element("Дегидроретинол",1500) },
            { 2, new Element("Тиамин",1500) },
            { 3, new Element("Рибофлавин",1800) },
            { 4, new Element("Никотинамид",20000) },
            { 5, new Element("Холин",450000) },
            { 6, new Element("Пантотеновая кислота",5000) },
            { 7, new Element("Придоксин",2000) },
            { 8, new Element("Биотин",50) },
            { 9, new Element("Инозитол",500) },
            { 10, new Element("Фолиевая кислота",400) },
            { 11, new Element("Дегидроретинол",1500) },
            { 12, new Element("Параминобензойная",0) },
            { 13, new Element("Лековарнитин",300000) },
            { 14, new Element("Цианокобаламин",3) },
            { 15, new Element("Оротовая кислота",1000) },
            { 16, new Element("Пангамовая кислота",100000) },
            { 17, new Element("Аскорбиновая кислота",90000) },
            { 18, new Element("Ламистерол",15) },
            { 19, new Element("Эргокальциферол",15) },
            { 20, new Element("Холекальциферол",15) },
            { 21, new Element("Дигидротахистерол",15) },
            { 22, new Element("7-Дигидротахистерол",15) },
            { 23, new Element("Токофероды",15000) },
            { 24, new Element("Филохинон",120) },
            { 25, new Element("Фарнохинон",120) },
            { 26, new Element("Липоевая кислота",30000) }
        };


        public int FindAvarage()
        {
            int i = number_of_elements;
            int avarage = 0;
            int index = 0;
            while (i > 0)
            {
                i -= (int)Math.Pow(2 ,index);
                index++;
                avarage++;
            }
            return avarage;
        }

        public void sorts()
        {
            int max = 0;
            int index = 0;
            eded = new int[number_of_elements];
            sorted_elements = new Element[number_of_elements];

            for (int i = 0; i < number_of_elements; i++)
            {

                foreach (int key in Deffults.Keys.ToArray())
                {

                    if (!eded.Contains(key))
                    {

                        if (max <= Deffults[key].avarage)
                        {
                            max = Deffults[key].avarage;
                            eded[i] = key;

                        }

                    }
                }
                max = 0;
            }
            for (int i = 0; i < number_of_elements; i++)
            {
                sorted_elements[i] = Deffults[i];
            }

            avarage = FindAvarage();
            ElementComparer myComparer = new ElementComparer();//Класс, реализующий сравнение
            heap = new Heap<Element>(sorted_elements, myComparer);

            heap.HeapSort();

        }


        public VitaminsData(Panel lay)
        {
            MainLayout = lay;
            sorts();

        }

        public void VievDefults(string name = "")
        {

            int l_number_of_elements = number_of_elements;

            sorts();
            int index = 0;

            AvaragePanel[] data_of_avarage_lay = new AvaragePanel[avarage];

            for (int avarage_curent = 1; avarage_curent <= avarage; avarage_curent++) {

                data_of_avarage_lay[avarage_curent-1] = new AvaragePanel(avarage_curent, avarage);
                data_of_avarage_lay[avarage_curent-1].BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
                for (int index_of_avarage_lay = 0; index_of_avarage_lay < (int)Math.Pow(2, avarage_curent - 1); index_of_avarage_lay++) {
                    if (index >= number_of_elements) {
                        break;
                    }
                    
                    AvaragePanelElement el = new AvaragePanelElement(index_of_avarage_lay, avarage_curent, avarage, heap._array[index].name, heap._array[index].avarage.ToString());

                    if (heap._array[index].name == name) {
                        el.BackColor = System.Drawing.Color.FromArgb(169, 200, 131);
                    }
                    data_of_avarage_lay[avarage_curent - 1].Controls.Add(el);
                    index++;
                }

                MainLayout.Controls.Add(data_of_avarage_lay[avarage_curent-1]);
            }




        }

        public void FindByName(string name)
        {

            MainLayout.Controls.Clear();
            bool is_name = false;
            foreach (int key in Deffults.Keys.ToArray())
            {
                if (name == Deffults[key].name)
                {
                    is_name = true;
                    break;
                } 
            }
            if (is_name)
            {
                VievDefults(name);
            }
            else { 
                VievDefults();
            }


        }

        public void DeleteByName(string name)
        {

            int index = 0;
            bool is_element = false;

            foreach (int key in Deffults.Keys.ToArray())
            {
                if (name == Deffults[key].name)
                {
                    index = key;
                    is_element = true;

                } 
            }
            if (is_element) {
                Deffults.Remove(index);

                for (int i = index + 1; i < number_of_elements; i++)
                {
                    var oldValue = Deffults[i];
                    Deffults.Remove(i);
                    Deffults.Add(i - 1, oldValue);
                }

                number_of_elements--;

                VievDefults();
            }


        }


        public void AddNewElement(string name, int avarage)
        {
            MainLayout.Controls.Clear();
            Deffults.Add(number_of_elements, new Element(name, avarage));
            number_of_elements++;


            VievDefults();
        }





    }

    internal class AvaragePanel : Panel
    {
        public AvaragePanel(int avarage, int max_avarage)
        {

            this.Name = avarage.ToString() + "panel";
            int x = 150 * ((int)Math.Pow(2, max_avarage - 1));
            int y = 60;
            this.Size = new System.Drawing.Size(x, y);
            this.Location = new System.Drawing.Point(10, y * (avarage - 1) + 20);
        }

    }

    internal class AvaragePanelElement : Panel
    {
        public AvaragePanelElement(int index,int curent_avarage, int avarage, string name, string day_norm)
        {
            int x = 150;
            int y = 60;

            int self_division = ((int)Math.Pow(2, avarage) - (int)Math.Pow(2, curent_avarage)) / 2 * 150;


            this.Name = "Index" + index.ToString() + "panelElementByAvarage" + avarage.ToString();
            this.Size = new System.Drawing.Size(x, y);
            this.Location = new System.Drawing.Point(index * 150, 0);

            Label name_element = new Label();
            name_element.Name = "Index" + index.ToString() + "panelNameElementByAvarage" + avarage.ToString();
            name_element.Text = name;
            name_element.Size = new System.Drawing.Size(150, 30);
            name_element.Location = new System.Drawing.Point(12, 5);


            Label daynormal_element = new Label();
            daynormal_element.Name = "Index" + index.ToString() + "panelNameElementByAvarage" + avarage.ToString();
            daynormal_element.Text = day_norm;
            daynormal_element.Size = new System.Drawing.Size(150, 30);
            daynormal_element.Location = new System.Drawing.Point(12,35);

            this.Controls.Add(name_element);
            this.Controls.Add(daynormal_element);

        }

    }

}
