using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum ConfigElementType
    {
        [EnumMember(Value = "INFO")] //     This is a static plain-text box that can be used to provide instructions or information to the user.Any links will be clickable.
        INFO = 0,

        [EnumMember(Value = "TEXTINPUT")] //   The input element will be a single-line text box.
        TEXTINPUT = 1,
        [EnumMember(Value = "TEXTAREA")] //     The input element will be a multi-line textarea box.
        TEXTAREA = 2,
        [EnumMember(Value = "CHECKBOX")] //     The input element will be a single checkbox that can be used to capture boolean values.
        CHECKBOX = 4,
        [EnumMember(Value = "SELECT_SINGLE")] //    The input element will be a dropdown for single-select options.
        SELECT_SINGLE = 64,
        [EnumMember(Value = "SELECT_MULTIPLE")] //  The input element will be a dropdown for multi-select options.
        SELECT_MULTIPLE = 1024,
    }

    public static class ConfigValueHelper {
        public static bool IsMultiValueInput(ConfigElementType elType) {
            var isMultiValued = false;
            if (elType >= ConfigElementType.SELECT_SINGLE) {
                isMultiValued = true;
            }
            return isMultiValued;
        }
    }
}
