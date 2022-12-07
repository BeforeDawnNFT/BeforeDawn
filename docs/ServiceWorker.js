const cacheName = "VRcollab-BeforeDawn-1.0.0";
const contentToCache = [
    "Build/build.loader.js",
    "Build/809c33e1a346ecae303f41b0c2bb838f.js.unityweb",
    "Build/f02a0b9473037178eae6179f1c800b6f.data.unityweb",
    "Build/a366dea681804c4a3a685f6de0135598.wasm.unityweb",
    "TemplateData/style.css"

];

self.addEventListener('install', function (e) {
    console.log('[Service Worker] Install');
    
    e.waitUntil((async function () {
      const cache = await caches.open(cacheName);
      console.log('[Service Worker] Caching all: app shell and content');
      await cache.addAll(contentToCache);
    })());
});

self.addEventListener('fetch', function (e) {
    e.respondWith((async function () {
      let response = await caches.match(e.request);
      console.log(`[Service Worker] Fetching resource: ${e.request.url}`);
      if (response) { return response; }

      response = await fetch(e.request);
      const cache = await caches.open(cacheName);
      console.log(`[Service Worker] Caching new resource: ${e.request.url}`);
      cache.put(e.request, response.clone());
      return response;
    })());
});
