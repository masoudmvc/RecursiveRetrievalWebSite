# RecursiveRetrievalWebSite
---------------------------
Creating a console program in C# that can recursively traverse and download a website and save it to disk while keeping the online file structure. It Shows download progress in the console.

Be noted. this project implemented based on the websites that every assests link define from root of project. if you want to use this project for relative path assets,it needs some changes in either downloded html and recursive method inside the project.

Due to the large number of requests, it takes a long time to download a website. So it is better to do this operation in parallel for each page, which I put a todo in the code where this should be done and I will do it soon. (//todo: this loop should be parallel loop (Parallel.For))
