using System.Collections.Generic;

namespace ImpBot.Modules.Search
{
    public class Metadata
    {
        public string Provider { get; set; }
    }

    public class DerivativeOf
    {
        public IList<string> Domains { get; set; }
        public string Id { get; set; }
        public string Language { get; set; }
        public IList<string> Regions { get; set; }
        public IList<string> Registers { get; set; }
        public string Text { get; set; }
    }

    public class CrossReference
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public partial class Example
    {
        public IList<string> Definitions { get; set; }
        public IList<string> Domains { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<string> Regions { get; set; }
        public IList<string> Registers { get; set; }
        public IList<string> SenseIds { get; set; }
        public string Text { get; set; }
        public IList<Translation> Translations { get; set; }
    }

    public class Subsense
    {
        public IList<string> Definitions { get; set; }
        public IList<Example> Examples { get; set; }
        public string Id { get; set; }
        public IList<string> Registers { get; set; }
        public IList<string> Domains { get; set; }
    }

    public class Translation
    {
        public IList<string> Domains { get; set; }
        public IList<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public string Language { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<string> Regions { get; set; }
        public IList<string> Registers { get; set; }
        public string Text { get; set; }
    }

    public class Sense
    {
        public IList<string> CrossReferenceMarkers { get; set; }
        public IList<CrossReference> CrossReferences { get; set; }
        public IList<string> Definitions { get; set; }
        public IList<string> Domains { get; set; }
        public IList<Example> Examples { get; set; }
        public string Id { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<Pronunciation> Pronunciations { get; set; }
        public IList<string> Regions { get; set; }
        public IList<string> Registers { get; set; }
        public IList<Subsense> Subsenses { get; set; }
        public IList<Translation> Translations { get; set; }
        public IList<VariantForm> VariantForms { get; set; }
    }

    public class Entry
    {
        public IList<string> Etymologies { get; set; }
        public IList<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public string HomographNumber { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<Pronunciation> Pronunciations { get; set; }
        public IList<Sense> Senses { get; set; }
        public IList<VariantForm> VariantForms { get; set; }
    }

    public class GrammaticalFeature
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class Note
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class VariantForm
    {
        public IList<string> Regions { get; set; }
        public string Text { get; set; }
    }

    public class LexicalEntry
    {
        public IList<DerivativeOf> DerivativeOf { get; set; }
        public IList<Entry> Entries { get; set; }
        public IList<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public string Language { get; set; }
        public string LexicalCategory { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<Pronunciation> Pronunciations { get; set; }
        public string Text { get; set; }
        public IList<VariantForm> VariantForms { get; set; }
    }

    public class Pronunciation
    {
        public string AudioFile { get; set; }
        public IList<string> Dialects { get; set; }
        public string PhoneticNotation { get; set; }
        public string PhoneticSpelling { get; set; }
        public IList<string> Regions { get; set; }
    }

    public class Result
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public IList<LexicalEntry> LexicalEntries { get; set; }
        public IList<Pronunciation> Pronunciations { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
    }

    public partial class Example
    {
        public Metadata Metadata { get; set; }
        public IList<Result> Results { get; set; }
    }

}
