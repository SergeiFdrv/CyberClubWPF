﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CyberClub.AppResources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Error {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Error() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CyberClub.AppResources.Error", typeof(Error).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Addition failed..
        /// </summary>
        internal static string AddError {
            get {
                return ResourceManager.GetString("AddError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Database error..
        /// </summary>
        internal static string DBError {
            get {
                return ResourceManager.GetString("DBError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на An error occured..
        /// </summary>
        internal static string Default {
            get {
                return ResourceManager.GetString("Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The program&apos;s executable file could not be found. The lazy ass admin should have set the proper file path..
        /// </summary>
        internal static string ExeNotFound {
            get {
                return ResourceManager.GetString("ExeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Fill in all the reqiured textfields..
        /// </summary>
        internal static string FillInNecessaryTextBox {
            get {
                return ResourceManager.GetString("FillInNecessaryTextBox", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The login-password combination could not be found. Contact the admin to register an account..
        /// </summary>
        internal static string LoginPasswordNotFound {
            get {
                return ResourceManager.GetString("LoginPasswordNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Process failed. Possibly, some required parameters are not set or the chosen name is already used..
        /// </summary>
        internal static string MissingParamsOrNameAlreadyUsed {
            get {
                return ResourceManager.GetString("MissingParamsOrNameAlreadyUsed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The name is already used..
        /// </summary>
        internal static string NameAlreadyUsed {
            get {
                return ResourceManager.GetString("NameAlreadyUsed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The name is not entered..
        /// </summary>
        internal static string NameNotEntered {
            get {
                return ResourceManager.GetString("NameNotEntered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Select the genre to rename and input the new name..
        /// </summary>
        internal static string SelectAGenreNTypeTheName {
            get {
                return ResourceManager.GetString("SelectAGenreNTypeTheName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Select the genre to delete..
        /// </summary>
        internal static string SelectAGenreToDelete {
            get {
                return ResourceManager.GetString("SelectAGenreToDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Seems like your account has been suspended. The admin has more info..
        /// </summary>
        internal static string YouAreBanned {
            get {
                return ResourceManager.GetString("YouAreBanned", resourceCulture);
            }
        }
    }
}
