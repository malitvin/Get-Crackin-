var TextUploaderPlugin = {
  TextUploaderCaptureClick: function() {
    if (!document.getElementById('TextUploaderInput')) {
      var fileInput = document.createElement('input');
      fileInput.setAttribute('type', 'file');
      fileInput.setAttribute('id', 'TextUploaderInput');
      fileInput.setAttribute('accept', 'text/txt');
      fileInput.style.visibility = 'hidden';
      fileInput.onclick = function (event) {
        this.value = null;
      };
      fileInput.onchange = function (event) {
        SendMessage('MYGAMEOBJECT', 'SavedFileSelected', URL.createObjectURL(event.target.files[0]));
      }
      document.body.appendChild(fileInput);
    }
    var OpenFileDialog = function() {
      document.getElementById('TextUploaderInput').click();
      document.getElementById('canvas').removeEventListener('click', OpenFileDialog);
    };
    document.getElementById('canvas').addEventListener('click', OpenFileDialog, false);
  }
};
mergeInto(LibraryManager.library, TextUploaderPlugin);