﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Interop.UIAutomationClient;

namespace dDeltaSolutions.Spy
{
    public partial class MainWindow
    {
        private void GetCurrentInformation(TreeNode node)
        {
            if (PropertySettings.hasAcceleratorKey)
            {
                try
                {
                    string acceleratorKey = "\"" + node.Element.CurrentAcceleratorKey + "\"";
                    Attribute attribute = new Attribute("Accelerator Key:", acceleratorKey)
                    {
                        Tooltip = "String containing the accelerator key combinations for the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasAccessKey)
            {
                try
                {
                    string accessKey = "\"" + node.Element.CurrentAccessKey + "\"";
                    Attribute attribute = new Attribute("Access Key:", accessKey)
                    {
                        Tooltip = "String containing the access key character for the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasAriaProperties)
            {
                try
                {
                    string ariaProperties = "\"" + node.Element.CurrentAriaProperties + "\"";
                    Attribute attribute = new Attribute("Aria Properties:", ariaProperties)
                    {
                        Tooltip = "Accessible Rich Internet Applications (ARIA) properties of the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasAriaRole)
            {
                try
                {
                    string ariaRole = "\"" + node.Element.CurrentAriaRole + "\"";
                    Attribute attribute = new Attribute("Aria Role:", ariaRole)
                    {
                        Tooltip = "Accessible Rich Internet Applications (ARIA) role of the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //get automation id
            if (PropertySettings.hasAutomationId)
            {
                try
                {
                    string automationIdString = "\"" + node.Element.CurrentAutomationId + "\"";
                    Attribute attribute = new Attribute("AutomationId:", automationIdString)
                    {
                        Tooltip = "String containing the UI Automation identifier (ID) for the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //get bounding rectangle
            if (PropertySettings.hasBoundingRectangle)
            {
                try
                {
                    tagRECT boundingRectangle = node.Element.CurrentBoundingRectangle;
                    string rectangleString = RectangleToString(boundingRectangle);
                    Attribute attribute = new Attribute("Bounding rectangle:", rectangleString)
                    {
                        Tooltip = "Coordinates of the rectangle that completely encloses the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //get window class
            if (PropertySettings.hasClassName)
            {
                try
                {
                    string windowClass = "\"" + node.Element.CurrentClassName + "\"";
                    Attribute attribute = new Attribute("Class Name:", windowClass)
                    {
                        Tooltip = "String containing the class name of the element as assigned by the control developer"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //clickable point
            if (PropertySettings.hasClickablePoint)
            {
                try
                {
                    //bool skip = false;
                    if (IsWindows7() && CompareElements(node.Element, uiAutomation.GetRootElement()))
                    {
                        // On Windows 7 don't get ClickablePoint for Desktop
                    }
                    else
                    {
                        tagPOINT clickablePoint; //= new tagRECT();
                        if (node.Element.GetClickablePoint(out clickablePoint) != 0)
                        {
                            string pointString = GetStringFromPoint(clickablePoint);
                            Attribute attribute = new Attribute("ClickablePoint:", pointString)
                            {
                                Tooltip = "Point on the Automation Element that can be clicked"
                            };
                            listAttributes.Add(attribute);
                        }
                    }
                }
                catch { }
            }

            if (PropertySettings.hasControllerFor)
            {
                try
                {
                    IUIAutomationElementArray array = node.Element.CurrentControllerFor;
                    for (int i = 0; i < array.Length; i++)
                    {
                        IUIAutomationElement element = array.GetElement(i);
                        TreeNode elementNode = new TreeNode(element);
                        string controllerForString = elementNode.ToString();
                        Attribute attribute = new Attribute("ControllerFor[" + i + "]:", controllerForString)
                        {
                            Tooltip = "Array of elements for which this element serves as the controller",
                            UnderneathElement = element
                        };
                        listAttributes.Add(attribute);
                    }
                }
                catch { }
            }

            //get Control Type
            if (PropertySettings.hasControlType)
            {
                try
                {
                    int controlTypeId = node.Element.CurrentControlType;
                    string controlTypeString = Helper.ControlTypeIdToString(controlTypeId) + " (" + controlTypeId + ")";
                    Attribute attribute = new Attribute("Control type:", controlTypeString)
                    {
                        Tooltip = "Control type of the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasCulture)
            {
                try
                {
                    int culture = node.Element.CurrentCulture;
                    Attribute attribute = new Attribute("Culture:", culture.ToString())
                    {
                        Tooltip = "Culture identifier"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasDescribedBy)
            {
                try
                {
                    IUIAutomationElementArray array = node.Element.CurrentDescribedBy;
                    for (int i = 0; i < array.Length; i++)
                    {
                        IUIAutomationElement element = array.GetElement(i);
                        TreeNode elementNode = new TreeNode(element);
                        string describedByString = elementNode.ToString();
                        Attribute attribute = new Attribute("DescribedBy[" + i + "]:", describedByString)
                        {
                            Tooltip = "Array of elements that describe this element",
                            UnderneathElement = element
                        };
                        listAttributes.Add(attribute);
                    }
                }
                catch { }
            }

            if (PropertySettings.hasFlowsTo)
            {
                try
                {
                    IUIAutomationElementArray array = node.Element.CurrentFlowsTo;
                    for (int i = 0; i < array.Length; i++)
                    {
                        IUIAutomationElement element = array.GetElement(i);
                        TreeNode elementNode = new TreeNode(element);
                        string flowsToString = elementNode.ToString();
                        Attribute attribute = new Attribute("FlowsTo[" + i + "]:", flowsToString)
                        {
                            Tooltip = "Array of elements that indicates the reading order after the current element",
                            UnderneathElement = element
                        };
                        listAttributes.Add(attribute);
                    }
                }
                catch { }
            }

            if (PropertySettings.hasFrameworkId)
            {
                try
                {
                    string frameworkIdString = node.Element.CurrentFrameworkId;
                    Attribute attribute = new Attribute("FrameworkId:", "\"" + frameworkIdString + "\"")
                    {
                        Tooltip = "Name of the underlying UI framework"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasHasKeyboardFocus)
            {
                try
                {
                    int hasKeyboboardFocus = node.Element.CurrentHasKeyboardFocus;
                    Attribute attribute = new Attribute("HasKeyboardFocus:", hasKeyboboardFocus.ToString())
                    {
                        Tooltip = "Value that indicates whether the element has keyboard focus"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasHelpText)
            {
                try
                {
                    string helpText = "\"" + node.Element.CurrentHelpText + "\"";
                    Attribute attribute = new Attribute("Help Text:", helpText)
                    {
                        Tooltip = "Help text associated with the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsContentElement)
            {
                try
                {
                    string isContentString = node.Element.CurrentIsContentElement.ToString();
                    Attribute attribute = new Attribute("IsContentElement:", isContentString)
                    {
                        Tooltip = "Value that specifies whether the element is a content element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsControlElement)
            {
                try
                {
                    string isControlString = node.Element.CurrentIsControlElement.ToString();
                    Attribute attribute = new Attribute("IsControlElement:", isControlString)
                    {
                        Tooltip = "Value that indicates whether the element is viewed as a control"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsDataValidForForm)
            {
                try
                {
                    string isDataValidForFormString = node.Element.CurrentIsDataValidForForm.ToString();
                    Attribute attribute = new Attribute("IsDataValidForForm:", isDataValidForFormString)
                    {
                        Tooltip = "Indicates whether the element contains valid data for a form"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsEnabled)
            {
                try
                {
                    string isEnabledString = node.Element.CurrentIsEnabled.ToString();
                    Attribute attribute = new Attribute("IsEnabled:", isEnabledString)
                    {
                        Tooltip = "Value that indicates whether the element is enabled"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsKeyboardFocusable)
            {
                try
                {
                    string isKeyboardFocusableString = node.Element.CurrentIsKeyboardFocusable.ToString();
                    Attribute attribute = new Attribute("IsKeyboardFocusable:", isKeyboardFocusableString)
                    {
                        Tooltip = "Value that indicates whether the UI Automation element can accept keyboard focus"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsOffscreen)
            {
                try
                {
                    string isOffscreenString = node.Element.CurrentIsOffscreen.ToString();
                    Attribute attribute = new Attribute("IsOffscreen:", isOffscreenString)
                    {
                        Tooltip = "Value that indicates whether the UI Automation element is visible on the screen"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsPassword)
            {
                try
                {
                    string isPasswordString = node.Element.CurrentIsPassword.ToString();
                    Attribute attribute = new Attribute("IsPassword:", isPasswordString)
                    {
                        Tooltip = "Value that indicates whether the UI Automation element contains protected content"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasIsRequiredForForm)
            {
                try
                {
                    string isRequiredForFormString = node.Element.CurrentIsRequiredForForm.ToString();
                    Attribute attribute = new Attribute("IsRequiredForForm:", isRequiredForFormString)
                    {
                        Tooltip = "Indicates whether the UI Automation element is required to be filled out on a form"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasItemStatus)
            {
                try
                {
                    string itemStatusString = "\"" + node.Element.CurrentItemStatus + "\"";
                    Attribute attribute = new Attribute("ItemStatus:", itemStatusString)
                    {
                        Tooltip = "Description of the status of an item within an element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasItemType)
            {
                try
                {
                    string itemTypeString = "\"" + node.Element.CurrentItemType + "\"";
                    Attribute attribute = new Attribute("ItemType:", itemTypeString)
                    {
                        Tooltip = "Description of the type of an item"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasLabeledBy)
            {
                try
                {
                    IUIAutomationElement labeledBy = node.Element.CurrentLabeledBy;

                    if (labeledBy != null)
                    {
                        TreeNode nodeLabeledBy = new TreeNode(labeledBy);
                        string labeledByString = nodeLabeledBy.ToString();
                        Attribute attribute = new Attribute("Labeled by:", labeledByString)
                        {
                            Tooltip = "Element that contains the text label for this element",
                            UnderneathElement = labeledBy
                        };
                        listAttributes.Add(attribute);
                    }
                }
                catch { }
            }

            //get control type
            if (PropertySettings.hasLocalizedControlType)
            {
                try
                {
                    string localizedType = "\"" + node.Element.CurrentLocalizedControlType + "\"";
                    Attribute attribute = new Attribute("Localized control type:", localizedType)
                    {
                        Tooltip = "Gets a description of the control type"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //get name
            if (PropertySettings.hasName)
            {
                try
                {
                    string name = node.Element.CurrentName;

                    if (name != null && name.Length > maxNameLength)
                    {
                        name = name.Substring(0, maxNameLength);
                        name += "...";
                    }

                    name = "\"" + name + "\"";
                    Attribute attribute = new Attribute("Name:", name)
                    {
                        Tooltip = "Name of the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //get window handle
            if (PropertySettings.hasNativeWindowHandle)
            {
                try
                {
                    string windowHandleString = "";

                    IntPtr hwnd = node.Element.CurrentNativeWindowHandle;
                    if (hwnd != IntPtr.Zero)
                    {
                        StringBuilder sbName = new StringBuilder(256);
                        GetWindowText(hwnd, sbName, 256);

                        StringBuilder sbClassName = new StringBuilder(256);
                        GetClassName(hwnd, sbClassName, 256);

                        windowHandleString = "0x" + hwnd.ToString("X") +
                            ", WindowText: \"" + sbName + "\", WindowClass: \"" + sbClassName + "\"";
                    }
                    else
                    {
                        windowHandleString = "0x0";
                    }

                    Attribute attribute = new Attribute("Native Window handle:", windowHandleString)
                    {
                        Tooltip = "Handle of the element's window"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasOrientation)
            {
                try
                {
                    string orientationString = node.Element.CurrentOrientation.ToString();
                    Attribute attribute = new Attribute("Orientation:", orientationString)
                    {
                        Tooltip = "Orientation of the control"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //get process id
            if (PropertySettings.hasProcessId)
            {
                try
                {
                    int pid = node.Element.CurrentProcessId;
                    Process process = Process.GetProcessById(pid);
                    string processString = process.ProcessName + ".exe (PID: " + pid + ")";
                    Attribute attribute = new Attribute("Process:", processString)
                    {
                        Tooltip = "Process identifier (ID) of this element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            if (PropertySettings.hasProviderDescription)
            {
                try
                {
                    string providerDescription = node.Element.CurrentProviderDescription;
                    Attribute attribute = new Attribute("Provider Description:", providerDescription)
                    {
                        Tooltip = "Description of the provider for this element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            //get runtime id
            if (PropertySettings.hasRuntimeId)
            {
                try
                {
                    int[] runtimeId = node.Element.GetRuntimeId();
                    //string[] runtimeIdStrings = ArrayToStringArray(runtimeId);

                    //string runtimeIdString = "[" + string.Join(",", runtimeIdStrings) + "]";
                    string runtimeIdString = "[" + IntArrayToString(runtimeId) + "]";
                    Attribute attribute = new Attribute("RuntimeId:", runtimeIdString)
                    {
                        Tooltip = "Unique identifier assigned to the user interface (UI) item"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            #region supported patterns
            try
            {
                FillPatternsDictionary();

                string supportedPatterns = string.Empty;
                foreach (int patternId in patternsIds.Keys)
                {
                    try
                    {
                        if (node.Element.GetCurrentPattern(patternId) != null)
                        {
                            patternsIds[patternId].IsSupported = true;

                            if (supportedPatterns != string.Empty)
                            {
                                supportedPatterns += Environment.NewLine;
                            }

                            supportedPatterns += patternsIds[patternId];
                        }
                    }
                    catch { }
                }

                Attribute attribute = new Attribute("Supported Patterns:", supportedPatterns)
                {
                    Tooltip = "Control patterns that this Automation Element supports"
                };
                listAttributes.Add(attribute);
            }
            catch { }
            #endregion

            Attribute emptyAttribute = new Attribute("", "");
            listAttributes.Add(emptyAttribute);

            IUIAutomationElement2 element2 = node.Element as IUIAutomationElement2;
            if (element2 != null)
            {
                Attribute attribute = new Attribute("============", "IUIAutomationElement2 ========")
                {
                    Tooltip = "Properties added in IUIAutomationElement2 interface"
                };
                listAttributes.Add(attribute);

                try
                {
                    int optimizeForVisualContent = element2.CurrentOptimizeForVisualContent;
                    attribute = new Attribute("OptimizeForVisualContent:", optimizeForVisualContent.ToString())
                    {
                        Tooltip = "Indicates whether the provider exposes only elements that are visible"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }

                try
                {
                    LiveSetting liveSetting = element2.CurrentLiveSetting;
                    attribute = new Attribute("LiveSetting:", liveSetting.ToString())
                    {
                        Tooltip = "Indicates the type of notifications, if any, that the element sends when the content of the element changes"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            IUIAutomationElement3 element3 = node.Element as IUIAutomationElement3;
            if (element3 != null)
            {
                Attribute attribute = new Attribute("============", "IUIAutomationElement3 ========")
                {
                    Tooltip = "Properties added in IUIAutomationElement3 interface"
                };
                listAttributes.Add(attribute);

                try
                {
                    int isPeripheral = element3.CurrentIsPeripheral;
                    attribute = new Attribute("IsPeripheral:", isPeripheral.ToString())
                    {
                        Tooltip = "The current peripheral UI indicator for the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            IUIAutomationElement4 element4 = node.Element as IUIAutomationElement4;
            if (element4 != null)
            {
                Attribute attribute = new Attribute("============", "IUIAutomationElement4 ========")
                {
                    Tooltip = "Properties added in IUIAutomationElement4 interface"
                };
                listAttributes.Add(attribute);

                try
                {
                    int positionInSet = element4.CurrentPositionInSet;
                    attribute = new Attribute("PositionInSet:", positionInSet.ToString())
                    {
                        Tooltip = "The current 1-based integer for the ordinal position in the set for the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }

                try
                {
                    int sizeOfSet = element4.CurrentSizeOfSet;
                    attribute = new Attribute("SizeOfSet:", sizeOfSet.ToString())
                    {
                        Tooltip = "The current 1-based integer for the size of the set where the element is located"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }

                try
                {
                    int level = element4.CurrentLevel;
                    attribute = new Attribute("Level:", level.ToString())
                    {
                        Tooltip = "The current 1-based integer for the level (hierarchy) for the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }

                try
                {
                    int[] annotationTypes = element4.CurrentAnnotationTypes;
                    //MessageBox.Show("Length: " + annotationTypes.Length);
                    attribute = new Attribute("AnnotationTypes:", "[" + IntArrayToString(annotationTypes) + "]")
                    {
                        Tooltip = "The current list of annotation types associated with this element, such as comment, header, footer, and so on"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            IUIAutomationElement5 element5 = node.Element as IUIAutomationElement5;
            if (element5 != null)
            {
                Attribute attribute = new Attribute("============", "IUIAutomationElement5 ========")
                {
                    Tooltip = "Properties added in IUIAutomationElement5 interface"
                };
                listAttributes.Add(attribute);

                try
                {
                    int landmarkType = element5.CurrentLandmarkType;
                    string landmarkTypeString = null;
                    if (LandmarkTypesDict.ContainsKey(landmarkType))
                    {
                        landmarkTypeString = LandmarkTypesDict[landmarkType];
                    }
                    else
                    {
                        landmarkTypeString = landmarkType.ToString();
                    }
                    attribute = new Attribute("LandmarkType:", landmarkTypeString)
                    {
                        Tooltip = "The current landmark type ID for the automation element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }

                try
                {
                    string localizedLandmarkType = element5.CurrentLocalizedLandmarkType;
                    attribute = new Attribute("LocalizedLandmarkType:", "\"" + localizedLandmarkType + "\"")
                    {
                        Tooltip = "A string containing the current localized landmark type for the automation element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            IUIAutomationElement6 element6 = node.Element as IUIAutomationElement6;
            if (element6 != null)
            {
                Attribute attribute = new Attribute("============", "IUIAutomationElement6 ========")
                {
                    Tooltip = "Properties added in IUIAutomationElement6 interface"
                };
                listAttributes.Add(attribute);

                try
                {
                    string fullDescription = element6.CurrentFullDescription;
                    attribute = new Attribute("FullDescription:", "\"" + fullDescription + "\"")
                    {
                        Tooltip = "The current full description of the automation element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            IUIAutomationElement8 element8 = node.Element as IUIAutomationElement8;
            if (element8 != null)
            {
                Attribute attribute = new Attribute("============", "IUIAutomationElement8 ========")
                {
                    Tooltip = "Properties added in IUIAutomationElement8 interface"
                };
                listAttributes.Add(attribute);

                try
                {
                    int headingLevel = element8.CurrentHeadingLevel;
                    attribute = new Attribute("HeadingLevel:", HeadingLevelsDict[headingLevel])
                    {
                        Tooltip = "The current heading level of the automation element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }

            IUIAutomationElement9 element9 = node.Element as IUIAutomationElement9;
            if (element9 != null)
            {
                Attribute attribute = new Attribute("============", "IUIAutomationElement9 ========")
                {
                    Tooltip = "Properties added in IUIAutomationElement9 interface"
                };
                listAttributes.Add(attribute);

                try
                {
                    int isDialog = element9.CurrentIsDialog;
                    attribute = new Attribute("IsDialog:", isDialog.ToString())
                    {
                        Tooltip = "The current is dialog window indicator for the element"
                    };
                    listAttributes.Add(attribute);
                }
                catch { }
            }
        }

        private static Dictionary<int, string> HeadingLevelsDict = new Dictionary<int, string>
        {
            { UIA_HeadingLevelIds.HeadingLevel_None, "HeadingLevel_None" },
            { UIA_HeadingLevelIds.HeadingLevel1, "HeadingLevel1" },
            { UIA_HeadingLevelIds.HeadingLevel2, "HeadingLevel2" },
            { UIA_HeadingLevelIds.HeadingLevel3, "HeadingLevel3" },
            { UIA_HeadingLevelIds.HeadingLevel4, "HeadingLevel4" },
            { UIA_HeadingLevelIds.HeadingLevel5, "HeadingLevel5" },
            { UIA_HeadingLevelIds.HeadingLevel6, "HeadingLevel6" },
            { UIA_HeadingLevelIds.HeadingLevel7, "HeadingLevel7" },
            { UIA_HeadingLevelIds.HeadingLevel8, "HeadingLevel8" },
            { UIA_HeadingLevelIds.HeadingLevel9, "HeadingLevel9" }
        };

        private static Dictionary<int, string> LandmarkTypesDict = new Dictionary<int, string>
        {
            { UIA_LandmarkTypeIds.UIA_CustomLandmarkTypeId, "UIA_CustomLandmarkTypeId" },
            { UIA_LandmarkTypeIds.UIA_FormLandmarkTypeId, "UIA_FormLandmarkTypeId" },
            { UIA_LandmarkTypeIds.UIA_MainLandmarkTypeId, "UIA_MainLandmarkTypeId" },
            { UIA_LandmarkTypeIds.UIA_NavigationLandmarkTypeId, "UIA_NavigationLandmarkTypeId" },
            { UIA_LandmarkTypeIds.UIA_SearchLandmarkTypeId, "UIA_SearchLandmarkTypeId" }
        };
    }
}