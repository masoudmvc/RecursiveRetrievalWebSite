# RecursiveRetrievalWebSite
---------------------------
a console program in C# that can recursively traverse and download a website and save it to disk while keeping the online file structure. It Shows download progress in the console.

Be noted. this project implemented based on the websites that every assests link define from root of project. if you want to use this project for relative path assets,it needs some changes in either downloded html and recursive method inside the project.

Due to the large number of requests, it takes a long time to download a website. So it is better to do this operation in parallel for each page, which I put a todo in the code where this should be done and I will do it soon. (//todo: this loop should be parallel loop (Parallel.For))

# the thoughts behind (Algorithm)
---------------------------------
In the first step, I take the html content of the first page of the website, Then I scan the content to fetch links (<a>) and elements that shows css, image and js path. so I have 4 list. first I download the css, image and js in the correct path (based on the href path) and then recursively I do the same for the html list. the condition of the recursive method is when an html doesn't have any internal link (<a>). during this process I have a general list to keep the downloded files name and for any html I check if it has not been downloaded before. there are two reasons for that, if we don't do it maybe we stock in a infinit loop for example 2 pages have link to each other. also we prevent duplicate operation. for example some styles and images may be used in different html files.

# How to run
------------
just clone the project and run. the default destination folder is "C:\MasCodeChallange" that you can change it from "Program.cs".
