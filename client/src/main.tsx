import { Capacitor } from "@capacitor/core";
import { Router } from "wouter";
import { useHashLocation } from "wouter/use-hash-location";
import { useEffect } from "react";
import { trpc } from "@/lib/trpc";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { httpBatchLink } from "@trpc/client";
import { createRoot } from "react-dom/client";
import superjson from "superjson";
import App from "./App";
import "./index.css";

// Suprimir avisos benignos de ResizeObserver
const resizeObserverLoopErrRe = /^[^(ResizeObserver loop limit exceeded)]/;
const resizeObserverLoopErrRe2 = /^[^(ResizeObserver loop completed with undelivered notifications)]/;
window.addEventListener("error", (e) => {
  if (
    resizeObserverLoopErrRe.test(e.message) ||
    resizeObserverLoopErrRe2.test(e.message)
  ) {
    e.stopImmediatePropagation();
  }
});

const queryClient = new QueryClient();

const apiHost =
  Capacitor.isNativePlatform()
    ? (import.meta.env.VITE_API_URL &&
        import.meta.env.VITE_API_URL.trim().length > 0
        ? import.meta.env.VITE_API_URL
        : "http://10.0.2.2:3000")
    : "";

const baseApiUrl = Capacitor.isNativePlatform()
  ? `${apiHost.replace(/\/$/, "")}/api/trpc`
  : "/api/trpc";

const trpcClient = trpc.createClient({
  links: [
    httpBatchLink({
      url: baseApiUrl,
      transformer: superjson,
      fetch(input, init) {
        return globalThis.fetch(input, {
          ...(init ?? {}),
          credentials: "include",
        });
      },
    }),
  ],
});

function AppContainer() {
  useEffect(() => {
    if (!Capacitor.isNativePlatform()) return;
    import("@capacitor/splash-screen")
      .then(({ SplashScreen }) => SplashScreen.hide().catch(() => undefined))
      .catch(() => undefined);
  }, []);

  return <App />;
}

createRoot(document.getElementById("root")!).render(
  <trpc.Provider client={trpcClient} queryClient={queryClient}>
    <QueryClientProvider client={queryClient}>
      <Router hook={useHashLocation}>
        <AppContainer />
      </Router>
    </QueryClientProvider>
  </trpc.Provider>
);
