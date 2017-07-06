using System.ComponentModel;

namespace HaenggiModel.DeviceCommunication
{
    public enum SetupMode
    {
        [Description("Messure")]
        Messure = 1,

        [Description("One Data")]
        OneData = 2,

        [Description("Repetitive Data")]
        Repetitive = 3
    }
}
