using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Explorer.Logics;
using SolidFrame.Explorer.UI;

namespace SolidFrame.Explorer.Types
{
	public interface IExplorerServiceDependencies
	{
		IExplorerViewModel ExplorerViewModel { get; }
		ITranslationService TranslationService { get; }
		IExplorerItemViewModelFactory ExplorerItemViewModelFactory { get; }
	}

	public class ExplorerServiceDependencies : IExplorerServiceDependencies
	{
		public ExplorerServiceDependencies(ITranslationService translationService, 
			IExplorerItemViewModelFactory explorerItemViewModelFactory, 
			IExplorerViewModel explorerViewModel)
		{
			TranslationService = translationService;
			ExplorerItemViewModelFactory = explorerItemViewModelFactory;
			ExplorerViewModel = explorerViewModel;
		}

		public IExplorerViewModel ExplorerViewModel { get; private set; }
		public ITranslationService TranslationService { get; private set; }
		public IExplorerItemViewModelFactory ExplorerItemViewModelFactory { get; private set; }
	}
}
