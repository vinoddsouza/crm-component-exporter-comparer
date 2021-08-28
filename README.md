# crm-component-comparer-exporter

CRM Component Comparer and Exporter tool will help developers, system administrators, and business analysts to compare two Dynamics CRM environments and export components to manage version history.

**Major features**

-	Powerful Query Editor - define multiple queries, select the whole solution, pick and choose the components for comparison 
-	List view showing the components that are different, drill down, filter what is modified and what is not
-	Log view to show the progress 
-	Visually see the difference using built-in file comparer tool
-	Export components to manage version history

This tool was tested on Microsoft Dynamics 365 9.2 online version.

## Getting Started

-	Select the source and target environments
-	Click on the ‘Add’ button to define the query 
- Alternatively select previously exported zip file in order to compare with previous version instead of target environment

![Home Screen](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/home-screen.png?raw=true)

-	Select the solution to compare. And click the ‘Ok’ button. All components within the solution will be selected 

![Query Editor 1](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/query-editor-1.png?raw=true)

-	Alternatively, you can also select specific components using the name, schema name and wild cards.

![Query Editor 2](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/query-editor-2.png?raw=true)

-	Click the ‘Options’ button on the main screen to select additional settings. Ribbons are not included by default when comparing the tables. Select this option If you want to compare the ribbons.

![Query Options](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/query-options.png?raw=true)

-	Tool supports multiple queries. 
-	To Edit/Remove select the query and click on Edit/Remove button.
-	Click on the ‘compare’ button. 

![Compare Screen](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/compare-screen.png?raw=true)

-	Compare Result will pop up to show the list of files. You can drill down to view the files. 

![Compare Result 1](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/compare-result-1.png?raw=true)

  -	**Unchanged** – No changes are found 
  -	**Modified** – The component is different 
  -	**Only in source** – Component is found the source but not in the target environment (if the comparison is on a solution, it means the component is not found in the solution)
  -	**Only in target** – Component is found in target but not in the source environment (if the comparison is on a solution, it means the component is not found in the solution)

  > Use filter option on bottom left to update the filter on the screen.

<br>

-	Double click on a row to drilldown or open file comparison tool.
-	If you close the compare result screen accidentally, open it again by clicking the ‘View Last Result’ button on the main screen.

![Compare Result 2](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/compare-result-2.png?raw=true)

-	The difference is shown in for a security role 

![File Comparision View 1](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/file-comparision-view-1.png?raw=true)

-	The difference is shown in for a web resource 

![File Comparision View 2](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/file-comparision-view-2.png?raw=true)

**Export components (Source environment only)**

-	Export CRM components to save in file system.
-	Export helps to manage version history for single environment.

![Export Screen](https://github.com/vinoddsouza/crm-component-comparer-exporter/blob/main/screenshots/export-screen.png?raw=true)

## Query

### Query pattern

Pattern should follow wildcard format. See https://support.microsoft.com/en-us/office/use-wildcards-in-queries-and-parameters-in-access-ec057a45-78b1-4d16-8c20-242cde582e0b

### Query format

```
[Component]=[pattern1,[pattern2,[pattern3,...]]];[AnotherComponent]=[pattern1,[pattern2,[pattern3,...]]]
```

### Query Example

```
Table=contact,account,sale*;WebResource=contact.js
```

> Hint: Use `*` to include all component

> Hint: `Solution=[pattern1,[pattern2,...]]` will apply additional filter on query

> Note: In query pattern enter unique name of component. In case component doesn't support unique name than use display name.

### Supported components

- Table - Pattern should contains schema name of table.
- WebResource - Pattern should content schema name of webresource.
- PluginStep - Pattern should content display name of step.
- Choice - Pattern should content schema name of Choice.
- Dashboard - Pattern should content display name of dashboard.
- SiteMap - Pattern should content display name of sitemap.
- SecurityRole - Pattern should content display name of security role.
- Workflow - Pattern should content display name of workflow.
- BusinessRule - Pattern should content display name of businessrule.
- Action - Pattern should content display name of action.
- BusinessProcessFlow - Pattern should content display name of business process flow.
- ModelDrivenApp - Pattern should content display name of model driven app.

<!--
### Additional settings

- IncludeSystemWebresource - By adding this boolean property in query will allow to add system webresource (Not recommended).
- IncludeAllProperty - By adding this boolean property in query will allow to export all property of component (Not recommended).

### Query in CLI vs XrmToolBox

In CLI we need to build query as per doc but in XrmToolBox we can take benifits of QueryEditor.

## Export components

1. Select directory
2. Check `Delete directory`, if you wish to delete existing directory before export.
3. Click on `Export`.


## Compore component

This functionality support only for XrmToolBox.

1. Select Target
2. Click on `Compare`

### Configure compare tool

1. Click on setting
2. Go to Compare tab
3. Select compare tool (Right now support DiffMerge only)
4. Enter DiffMerge executable file path
-->

### Credit
Icon - https://www.flaticon.com/free-icon/ab-testing_4661446
Menees.Diffs https://github.com/menees/Diff.Net
