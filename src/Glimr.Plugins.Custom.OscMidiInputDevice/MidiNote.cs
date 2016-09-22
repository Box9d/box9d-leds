namespace Glimr.Plugins.Custom.OscMidiInputDevice
{
    public class MidiNote
    {
        public int Octave { get; }
        public Note Note { get; }

        private MidiNote(int octave, Note note)
        {
            Octave = octave;
            Note = note;
        }

        static internal MidiNote FromInteger(int bit)
        {
            var octave = (bit / 12) - 1;
            var note = (Note)(bit % 12);

            return new MidiNote(octave, note);
        }

        public override string ToString()
        {
            return Note.ToString() + Octave;
        }
    }
}
