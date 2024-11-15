//Реалізувати систему розумного будинку, яка включає в себе підсистеми:
//світло, термостат, система безпеки, система зрошення за допомогою патерну Facade.

using System;
using System.Text;

namespace Facade
{
    //SubsystemClassA
    public class Lights
    {
        public void TurnOn() => Console.WriteLine("Світло увімкнено");
        public void TurnOff() => Console.WriteLine("Світло вимкнено");
    }

    //SubsystemClassB
    public class Thermostat
    {
        public void SetTemperature(int temperature) => Console.WriteLine($"Встановлення температури на {temperature}°C");
    }

    //SubsystemClassC
    public class SecuritySystem
    {
        public void Arm() => Console.WriteLine("Система безпеки активована");
        public void Disarm() => Console.WriteLine("Система безпеки деактивована");
    }

    //SubsystemClassD
    public class SprinklerSystem
    {
        public void Activate() => Console.WriteLine("Система зрошення активована");
        public void Deactivate() => Console.WriteLine("Система зрошення деактивована");
    }

    //FacadeClass
    public class SmartHomeFacade
    {
        private readonly Lights _lights;
        private readonly Thermostat _thermostat;
        private readonly SecuritySystem _security;
        private readonly SprinklerSystem _sprinklers;

        public SmartHomeFacade(Lights lights, Thermostat thermostat, SecuritySystem security, SprinklerSystem sprinklers)
        {
            _lights = lights;
            _thermostat = thermostat;
            _security = security;
            _sprinklers = sprinklers;
        }

        public void LeaveHome()
        {
            Console.WriteLine("\nПідготовка до виходу з дому...");
            _lights.TurnOff();
            _thermostat.SetTemperature(18);
            _security.Arm();
            _sprinklers.Deactivate();
            Console.WriteLine("Будинок захищений, Ви можете йти\n");
        }

        public void ArriveHome()
        {
            Console.WriteLine("\nЛаскаво просимо додому!");
            _lights.TurnOn();
            _thermostat.SetTemperature(22);
            _security.Disarm();
            _sprinklers.Activate();
            Console.WriteLine("Будинок готовий для Вас\n");
        }
    }

    //ClientCode
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Lights lights = new Lights();
            Thermostat thermostat = new Thermostat();
            SecuritySystem security = new SecuritySystem();
            SprinklerSystem sprinklers = new SprinklerSystem();

            SmartHomeFacade smartHome = new SmartHomeFacade(lights, thermostat, security, sprinklers);

            smartHome.LeaveHome();
            smartHome.ArriveHome();

            Console.Read();
        }
    }
}