using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.HelperModels
{
    public class WordParagraph
    {
        public List<string> Texts { get; set; }
        public WordParagraphProperties TextProperties { get; set; }
    }
}
