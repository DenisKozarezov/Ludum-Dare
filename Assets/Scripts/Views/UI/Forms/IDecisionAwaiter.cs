using System.Threading.Tasks;

namespace Core.UI.Forms
{
    internal interface IDecisionAwaiter
    {
        Task<bool> AwaitForDecision();
        void SetLabel(string label);
        void SetDescription(string text);
    }
}
