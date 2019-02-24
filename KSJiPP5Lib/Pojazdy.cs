using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSJiPP5Lib
{
    public abstract class Pojazd
    {
        protected double predkosc;  //chcemy by kazda klasa dziedzicząca po pojezdzie miala to pole, ale nie bylo one widoczne dla innych klas
        protected abstract double MaxPredkosc { get; } //wymuszamy by to pole (dokladniej wlasciwosc, bo pola nie moga byc abstrakcyjne, ani wirtualne) bylo zdefiniowane przez kazda klase potomna
        protected bool pojazdWlaczony;
        public Pojazd()
        {
            predkosc = 0;
        }

        public abstract void Jedz();    //kazdy pojazd może jechać, ale musi osobno zaimplementować tą metodę
        public virtual void Stop()  //może jakaś klasa będzie chciała inaczej zaimplementować hamowanie
        {
            predkosc = 0;
        }
        public void Przyspiesz(double przyspieszenie)   //dla wartosci ujemnych zadziala jak zwalnianie
        {
            predkosc += przyspieszenie;
            if (predkosc >= MaxPredkosc)
            {
                if (PrzegrzanieSilnika != null)
                {
                    PrzegrzanieSilnika(predkosc,MaxPredkosc);
                }
            }
        }

        public event PrzegrzewanieSilnika PrzegrzanieSilnika;
        public delegate void PrzegrzewanieSilnika(double predkosc, double maxPredkosc);
    }

    public class Samochod:Pojazd
    {
        protected override double MaxPredkosc { get => 100; }

        public override void Jedz()
        {
            pojazdWlaczony = true;
            Console.WriteLine("Samochod jedzie");
        }
        public Samochod()
        {
            PrzegrzanieSilnika += Samochod_PrzegrzanieSilnika;
        }

        private void Samochod_PrzegrzanieSilnika(double predkosc, double maxPredkosc)
        {
            Console.WriteLine("Silnik samochodu przegrzewa sie.\n Maxymalna predkosc to "+maxPredkosc.ToString()+" a jedziesz " + predkosc.ToString());
        }
    }
    public class Motor : Pojazd
    {
        protected override double MaxPredkosc => 150;

        public override void Jedz()
        {
            pojazdWlaczony = true;
            Console.WriteLine("Motor jedzie");
        }
        public Motor()
        {
            PrzegrzanieSilnika += Motor_PrzegrzanieSilnika;
        }

        private void Motor_PrzegrzanieSilnika(double predkosc, double maxPredkosc)
        {
            Console.WriteLine("Zaraz sie rozbijesz");
        }
    }

}
