using Rug.Osc;

namespace Glimr.Plugins.Custom.OscMidiInputDevice
{
    public class MidiMessage
    {
        public MidiNote MidiNote { get; }

        public int Velocity { get; }

        private MidiMessage(MidiNote midiNote, int velocity)
        {
            MidiNote = midiNote;
            Velocity = velocity;
        }

        internal static MidiMessage FromOscPacket(OscPacket oscPacket)
        {
            var message = (OscMessage)oscPacket;

            return new MidiMessage(MidiNote.FromInteger((int)message[0]), (int)message[1]);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", MidiNote, Velocity);
        }
    }
}
