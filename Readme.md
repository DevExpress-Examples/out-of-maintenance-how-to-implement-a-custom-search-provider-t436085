<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/CustomSearchProvider/Form1.cs) (VB: [Form1.vb](./VB/CustomSearchProvider/Form1.vb))
<!-- default file list end -->
# How to: Implement a Custom Search Provider


<p>This example demonstrates how to create a custom search provider.</p>


<h3>Description</h3>

<p>To do this, design a class inheriting&nbsp;the&nbsp;<a href="https://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraMapInformationDataProviderBasetopic">InformationDataProviderBase</a>&nbsp;class&nbsp;and implement the CreateData() method in it. Then, design a class inheriting the&nbsp;<a href="https://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraMapIInformationDatatopic">IInformationData</a>&nbsp;interface and override its&nbsp;<a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapIInformationData_OnDataResponsetopic">OnDataResponse</a>&nbsp;event. Implement the Search(string keyword) method to provide custom search logic.</p>

<br/>


