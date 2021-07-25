# crm-component-comparer-exporter
## About
This tool will help in comparing various Dynamics CRM 365 components from different environments.
## Query

### Query pattern

Pattern should follow wildcard format. See https://support.microsoft.com/en-us/office/use-wildcards-in-queries-and-parameters-in-access-ec057a45-78b1-4d16-8c20-242cde582e0b

### Query format

```
[Component]=[pattern1,[pattern2,[pattern3,...]]];[AnotherComponent]=[pattern1,[pattern2,[pattern3,...]]]
```

### Query Example

```
Entity=contact,account,sale*;WebResource=contact.js
```

> Hint: Use `*` to include all component

> Hint: `Solution=[pattern1,[pattern2,...]]` will apply additional filter on query

> Note: In query pattern enter unique name of component. In case component doesn't support unique name than use display name.

### Supported components

- Entity - Pattern should contains logical name of entity.
- WebResource - Pattern should content unique name of webresource.
- PluginStep - Pattern should content display name of step.
- OptionSet - Pattern should content unique name of OptionSet.
- Dashboard - Pattern should content display name of dashboard.
- SiteMap - Pattern should content unique name of sitemap.
- SecurityRole - Pattern should content display name of security role.
- Workflow - Pattern should content display name of workflow.
- BusinessRule - Pattern should content display name of businessrule.
- Action - Pattern should content display name of action.
- BusinessProcessFlow - Pattern should content display name of business process flow.
- ModelDrivenApp - Pattern should content unique name of model driven app.

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

