//
// Copyright (c) 2010-2022 Antmicro
//
//  This file is licensed under the MIT License.
//  Full license text is available in 'licenses/MIT.txt'.
//
using Antmicro.Renode.Core;
using Antmicro.Renode.Core.Structure.Registers;
using Antmicro.Renode.Logging;
using Antmicro.Renode.Peripherals.Bus;

namespace Antmicro.Renode.Peripherals.Dynamic
{
    public class SubobjectTester : BasicDoubleWordPeripheral, IKnownSize
    {
        public SubobjectTester(Machine machine) : base(machine)
        {
            subobject = new Subobject(this);

            Registers.Control.Define(this)
                .WithValueField(0, 32, writeCallback: (_, value) => HandleWrite(value))
            ;
        }

        public long Size => 0x4;

        private void HandleWrite(uint value)
        {
            this.Log(LogLevel.Noisy, "Hello from object");
            subobject.Write(value);
        }

        private readonly Subobject subobject;

        private enum Registers
        {
            Control = 0
        }

        private class Subobject
        {
            public Subobject(SubobjectTester parent)
            {
                this.parent = parent;
            }

            public void Write(uint value)
            {
                parent.Log(LogLevel.Noisy, "Hello from sub-object");
            }

            private readonly SubobjectTester parent;
        }
    }
}
