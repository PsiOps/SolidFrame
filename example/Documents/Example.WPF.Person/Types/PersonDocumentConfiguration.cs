using Example.WPF.Person.UI;
using Example.WPF.Resources;
using SolidFrame.Core.Interfaces.Document;
using System;

namespace Example.WPF.Person.Types
{
	public interface IPersonDocumentConfiguration : IDocumentConfiguration
	{
	}

	public class PersonDocumentConfiguration : IPersonDocumentConfiguration
	{
		public Guid Id { get {return DocumentIdCatalog.PersonDocumentId;} }
		public string Name { get { return "Person"; } }
		public Type ViewType { get { return typeof (PersonView); } }
	}
}
