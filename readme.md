# QRFT
QR code file transfer between your terminals via LAN 
## build
> git clone  
> import into vs2019 and build
## usage
>- qrft receive ${file_path}  
>- qrft send ${files_path}

## commit  

- **do not** commit to master feature   
- commit to dev feature **with description**
- use git **pull** to get latest code every time  when your start to write your own code

``` bash
  git pull origin dev
  git add --all  
  git commit -m "${description}"  
  git push -u origin dev
```

## todo
- [x] transfer via cache
- [ ] transfer via stream
- [x] basic function receive and send
- [x] commandline parse and save to config
- [x] QR code display
- [x] terminal progress bar
- [x] send or zip multiple files to send
- [x] receive multiple files
- [ ] remote module
