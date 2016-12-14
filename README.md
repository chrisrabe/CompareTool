# CompareTool
This tool was originally created for a company called "RightCrowd" in Australia. It is used to compare configuration files from two databases.

### Purpose
This tool compares two folders containing XML files and then displays all the differences and similarities between the two folders. This tool should be able to compare any XML exports (assuming that it uses a syntax where there is one root and no attributes). The only file which needs to be customised is the look-up table. This look-up table converts any ambiguious exports to human readable format. For example, if you have a field called 'Operation'. If the value is '1', then it means that the operation is 'Update'. If the value is '2', then it means that the operation is 'Remove'. The look-up table simply converts 1 to Update and displays 'Update' in the view instead of a '1'.
