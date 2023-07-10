﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime : 16.0.0.0
//  
//     Les changements apportés à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace USite_Templating.Services.T4
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class TemplateHtml : TemplateHtmlBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("﻿");
            this.Write(@"<!DOCTYPE html>
<html lang=""en"">

<head>
   <meta charset=""UTF-8"">
   <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
   <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
   <link rel=""stylesheet"" href=""style.min.css"" />
   <meta name=""description"" content=""");
            
            #line 14 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Page.Description));
            
            #line default
            #line hidden
            this.Write("\">\r\n   <title>");
            
            #line 15 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Page.Name));
            
            #line default
            #line hidden
            this.Write("</title>\r\n</head>\r\n\r\n<body>\r\n    <nav>\r\n        ");
            
            #line 20 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"

            var menu = Page.Elements.FirstOrDefault(x => x.MenuId != null);
            if(menu != null)
            {
                
            
            #line default
            #line hidden
            this.Write("                ");
            
            #line 25 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerateElementCode(menu)));
            
            #line default
            #line hidden
            this.Write("\r\n                ");
            
            #line 26 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"

            }
        
            
            #line default
            #line hidden
            this.Write("    </nav>\r\n      ");
            
            #line 30 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"
 foreach (var element in Page.Elements.Where(x => x.MenuId == null)) { 
            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 31 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerateElementCode(element)));
            
            #line default
            #line hidden
            this.Write("\r\n      ");
            
            #line 32 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"
 } 
            
            #line default
            #line hidden
            this.Write("</body>     \r\n\r\n</html>\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 37 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"

private string GenerateElementCode(USite_Templating.Services.Dtos.ElementDto element)
{
    var htmlCode = string.Empty;

    switch (element.Type)
    {
        case USite_Templating.Services.Dtos.TypeElement.Block:
            htmlCode += "<div class=\"" + string.Join(" ", element.Styles.Select(s => s.Value)) + "\">";
            break;
        case USite_Templating.Services.Dtos.TypeElement.H1:
            htmlCode += "<h1 class=\"" + string.Join(" ", element.Styles.Select(s => s.Value)) + "\">" + element.Content.Replace("\n", "<br />") + "</h1>";
            break;
        case USite_Templating.Services.Dtos.TypeElement.Button:
            htmlCode += "<button class=\"" + string.Join(" ", element.Styles.Select(s => s.Value)) + "\">" + element.Content.Replace("\n", "<br />") + "</button>";
            break;
        case USite_Templating.Services.Dtos.TypeElement.Link:
            htmlCode += "<a href=\"" + element.PageName + ".html\" class=\"" + string.Join(" ", element.Styles.Select(s => s.Value)) + "\">" + element.Content.Replace("\n", "<br />") + "</a>";
            break;
        case USite_Templating.Services.Dtos.TypeElement.Image:
            htmlCode += "<img src=\"" + element.Url + "\" alt=\"" + element.Alt + "\" class=\"" + string.Join(" ", element.Styles.Select(s => s.Value)) + "\"/>";
            break;
    }

    if (element.ElementsChilds != null)
    {
        foreach (var childElement in element.ElementsChilds)
        {
            htmlCode += GenerateElementCode(childElement);
        }
    }

    if (element.Type == USite_Templating.Services.Dtos.TypeElement.Block)
    {
        htmlCode += "</div>";
    }

    return htmlCode;
}

        
        #line default
        #line hidden
        
        #line 1 "C:\Users\Admin\workspace\USite-Templating\USite-Templating.Services\T4\TemplateHtml.tt"

private global::USite_Templating.Services.Dtos.PageDto _PageField;

/// <summary>
/// Access the Page parameter of the template.
/// </summary>
private global::USite_Templating.Services.Dtos.PageDto Page
{
    get
    {
        return this._PageField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool PageValueAcquired = false;
if (this.Session.ContainsKey("Page"))
{
    this._PageField = ((global::USite_Templating.Services.Dtos.PageDto)(this.Session["Page"]));
    PageValueAcquired = true;
}
if ((PageValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Page");
    if ((data != null))
    {
        this._PageField = ((global::USite_Templating.Services.Dtos.PageDto)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class TemplateHtmlBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
