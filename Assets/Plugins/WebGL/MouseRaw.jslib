mergeInto(LibraryManager.library, {
  InitRawMouse: function() {
    var canvas = Module['canvas'];
    if (!canvas) return;

    // Initialize storage
    if (!Module.WebGLMouseRaw) {
        Module.WebGLMouseRaw = {
            rawDeltaX: 0,
            rawDeltaY: 0
        };
    }

    // Ensure pointer lock requested on click
    canvas.addEventListener("click", function() {
      canvas.requestPointerLock = canvas.requestPointerLock ||
                                  canvas.mozRequestPointerLock ||
                                  canvas.webkitRequestPointerLock;
      canvas.requestPointerLock();
    });

    canvas.addEventListener("mousemove", function(e) {
        if (!Module.WebGLMouseRaw) return;

        // Only accumulate when pointer is locked
        if (document.pointerLockElement === canvas) {
            Module.WebGLMouseRaw.rawDeltaX += e.movementX || 0;
            Module.WebGLMouseRaw.rawDeltaY += -(e.movementY || 0);
        }
    });
  },

  ResetRawMouseDelta: function() {
      if (Module.WebGLMouseRaw) {
          Module.WebGLMouseRaw.rawDeltaX = 0;
          Module.WebGLMouseRaw.rawDeltaY = 0;
      }
  },

  GetRawMouseDeltaX: function() {
      return Module.WebGLMouseRaw ? Module.WebGLMouseRaw.rawDeltaX : 0;
  },

  GetRawMouseDeltaY: function() {
      return Module.WebGLMouseRaw ? Module.WebGLMouseRaw.rawDeltaY : 0;
  }
});