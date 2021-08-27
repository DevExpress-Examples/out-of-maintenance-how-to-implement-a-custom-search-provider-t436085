<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128576538/16.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T436085)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/CustomSearchProvider/Form1.cs) (VB: [Form1.vb](./VB/CustomSearchProvider/Form1.vb))
<!-- default file list end -->
# How to: Implement a Custom Search Provider


<p>This example demonstrates how to create a custom search provider.</p>


<h3>Description</h3>

<p>To do this, design a class inheriting&nbsp;the&nbsp;<a href="https://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraMapInformationDataProviderBasetopic">InformationDataProviderBase</a>&nbsp;class&nbsp;and implement the CreateData() method in it. Then, design a class inheriting the&nbsp;<a href="https://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraMapIInformationDatatopic">IInformationData</a>&nbsp;interface and override its&nbsp;<a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapIInformationData_OnDataResponsetopic">OnDataResponse</a>&nbsp;event. Implement the Search(string keyword) method to provide custom search logic.</p>

<br/>


