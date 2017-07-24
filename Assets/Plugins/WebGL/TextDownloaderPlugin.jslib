var TextDownloaderPlugin = {
  TextDownloader: function(str, fn) {
      var msg = Pointer_stringify(str);
      var fname = Pointer_stringify(fn);
      var data = new Blob([msg], {type: 'text/plain'});
      var link = document.createElement('a');
      link.download = fname;
      link.innerHTML = 'DownloadFile';
      link.setAttribute('id', 'TextDownloaderLink');
      if(window.webkitURL != null)
      {
          link.href = window.webkitURL.createObjectURL(data);
      }
      else
      {
          link.href = window.URL.createObjectURL(data);
          link.onclick = function()
          {
              var child = document.getElementById('TextDownloaderLink');
              child.parentNode.removeChild(child);
          };
          link.style.display = 'none';
          document.body.appendChild(link);
      }
      link.click();
  }
};
mergeInto(LibraryManager.library, TextDownloaderPlugin);