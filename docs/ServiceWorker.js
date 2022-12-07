const cacheName = "VRcollab-BeforeDawn-1.0.0";
const contentToCache = [
    "Build/build.loader.js",
    "Build/fc33993b4317f24d772b8ec8054906e1.js.unityweb",
    "Build/5fc8e761d56cc4a5e5c34c6e77846583.data.unityweb",
    "Build/3fe66fa6731b0adcce57de3bff2d3c41.wasm.unityweb",
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
