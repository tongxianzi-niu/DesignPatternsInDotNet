using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciples
{
    internal class InterfaceSegregationPrinciple
    {
        // Clients should not be forced to depend on methods they do not use
        // The dependency of one class to another one should depend on the smallest possible interface
        // 要为各个类建立它们需要的专用接口，而不要试图去建立一个很庞大的接口供所有依赖它的类去调用
    }

    // 假如有一个门（Door），有锁门（lock）和开锁（unlock）功能。此外，可以在门上安装一个报警器而使其具有报警（alarm）功能。
    // 用户可以选择一般的门，也可以选择具有报警功能的门。分析需求，找出其中的名词，我们不难得到三个候选类：
    // 门（Door）、普通门（CommonDoor）、有报警功能的门（AlarmDoor）。

    #region Violate Interface Segregation Principle
    public interface IDoor
    {
        public bool IsLocked { get; set; }
        public void Lock();
        public void Unlock();
        public void Alarm();
    }

    public class CommonDoor : IDoor
    {
        public bool IsLocked { get; set; }

        public void Alarm()
        {
            // common dont have alarm but forced to implement this method
        }

        public void Lock()
        {
            this.IsLocked = true;
        }

        public void Unlock()
        {
            this.IsLocked = false;
        }
    }

    public class AlarmDoor : IDoor
    {
        public bool IsLocked { get; set; }

        public void Alarm()
        {
            Console.WriteLine("Alarm");
        }

        public void Lock()
        {
            this.IsLocked = true;
        }

        public void Unlock()
        {
            this.IsLocked = false;
        }
    }
    #endregion

    #region Follow Interface Segregation Principle
    public interface IBetterDoor
    {
        public bool IsLocked { get; set; }
        public void Lock();
        public void Unlock();
    }

    public interface IAlarm
    {
        public void Alarm();
    }

    // there is no unnecessary method in CommonDoor
    public class BetterCommonDoor : IBetterDoor
    {
        public bool IsLocked { get; set; }

        public void Lock()
        {
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }
    }

    public class BetterAlarmDoor : BetterCommonDoor, IAlarm
    {
        public void Alarm()
        {
            Console.WriteLine("Alarm");
        }
    }
    #endregion
}
