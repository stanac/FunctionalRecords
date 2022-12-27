namespace FunctionalRecords;

internal interface IChoice
{
    bool Is<T>();
    int SelectedIndex { get; }
    string SelectedTypeName { get; }
}