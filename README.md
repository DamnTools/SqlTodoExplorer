
"to-do" items can be marked as:
  - TODO
  - HACK
  - BUG
  - ASK

##Download

    git clone https://github.com/engageitservices/SqlTodoExplorer.git
    
##Build

In order to build the source, you need to install SQL Server Management Studio 2012 (also the Express version).

##Supported versions of SQL Server Management Studio
- SSMS 2012
- SSMS 2012 Express

Support for other versions are in the [issues] section.


##Install

- Build the solution SqlTodoExplorer.sln or download the [latest release](https://github.com/engageitservices/SqlTodoExplorer/releases)

- Copy the `DamnTools.SqlTodoExplorer.dll` created in the bin folder to the following folder (create it if not exists):

        C:\Program Files\DamnTools\SqlTodoExplorer

- Copy the `DamnTools.SqlTodoExplorer.AddIn` file (stored on \Source\SqlTodoExplorer folder) in the following folder  (create it if not exists):

        C:\ProgramData\Microsoft\SQL Server Management Studio\<SQL Server Version>\AddIns

        Microsoft SQL Server Management Studio 2012:
        C:\ProgramData\Microsoft\SQL Server Management Studio\11.0\AddIns
        
- Edit the copy of the .AddIn file and change the <Assembly> path:

        <Assembly>C:\Program Files\DamnTools\SqlTodoExplorer\DamnTools.SqlTodoExplorer.dll</Assembly>

After the setup, you will find a new menu command under the "Tools" menu of Sql Server Management Studio.

![SQL Todo Explorer menu command](https://raw.githubusercontent.com/wiki/DamnTools/SqlTodoExplorer/images/new_menu_command.png)

The add-in view is a floating (by default) panel in your Sql Server Management Studio. You can dock the panel, pin and move it as any other panel within the IDE.

![SQL Todo Explorer panel](https://raw.githubusercontent.com/wiki/DamnTools/SqlTodoExplorer/images/panel.png)

##Debug

In Visual Studio:

- Open the project properties of "SqlTodoExplorer" project (ALT+ENTER) and fill in the "Debug->Start external application" option with the SQL Management Studio executable (Ssms.exe) full path:

        C:\Program Files (x86)\Microsoft SQL Server\<SQL Server Version>\AddIns
\Tools\Binn\ManagementStudio\Ssms.exe

- Set the "Working directory" option to the Ssms.exe parent folder:

        C:\Program Files (x86)\Microsoft SQL Server\<SQL Server Version>\AddIns
\Tools\Binn\ManagementStudio\
		
- Close the project properties

- Go to menu "Debug->Exceptions"... and uncheck the "Thrown" on "PInvokeStackImbalance" and "IvalidVariant" under "Managed Debugging Assistant"

- Hit F5

##Features

- Get the list of "to-do" items (TODO, HACK, BUG, ASK) of the selected connection in a specific user database
- Filter by database
- Filter by "to-do" item type
- Choose from three layout results (Flat list, Grouped by "to-do" and Grouped by object type)
- Drill/collapse the result when the layout is a treeview
- When the layout is "Flat list", filter by as-you-type search directly in the comments
- Double click an item in order to get the ALTER statement of the selected object in a new query window
- Refresh the connection context
- Export the results as CSV or XML file

##How to contribute?

Your contributions to SqlTodoExplorer are very welcome. If you find a bug, please raise it as an issue. Even better fix it and send a pull request. If you like to help out with existing bugs and feature requests just check out the list of [issues] and grab and fix one.

###Contribution guideline
This project uses [GitHub flow] for pull requests.
So if you want to contribute, fork the repo, preferably create a local branch to avoid conflicts with other activities, fix an issue, run a build of the solution, and send a PR if all is green.

1. `master` must always be deployable.
2. **all changes** made through feature branches (pull-request + merge)
3. rebase to avoid/resolve conflicts; merge in to `master`

Please rebase your code on top of the latest commits.
Before working on your fork make sure you pull the latest so you work on top of the latest commits to avoid merge conflicts.
Also before sending the pull request please rebase your code as there is a chance there have been new commits pushed after you pulled last.
Please refer to [this guide](https://gist.github.com/jbenet/ee6c9ac48068889b0912#the-workflow) if you're new to git (thanks to [Juan Batiz-Benet](https://github.com/jbenet)).

##Authors

- Michael Denny ([@dennymic])
- Alessandro Alpi ([@suxstellino])

__Contributors__
- See the [contributor] section

##License

Engage It Services Sql Todo Explorer is released under the [MIT License].

##Icon

Icon and Logo created by [Daniela Malvisi]


[Daniela Malvisi]: https://it.linkedin.com/pub/daniela-malvisi/61/859/275
[MIT License]: https://github.com/engageitservices/SqlTodoExplorer/blob/master/LICENSE
[contributor]: https://github.com/engageitservices/SqlTodoExplorer/graphs/contributors
[@suxstellino]: https://twitter.com/suxstellino
[@dennymic]: https://twitter.com/dennymic
[issues]: https://github.com/engageitservices/SqlTodoExplorer/issues
[GitHub flow]: http://scottchacon.com/2011/08/31/github-flow.html
