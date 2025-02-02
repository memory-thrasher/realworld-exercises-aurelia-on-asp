import {HttpError} from "./HttpError";

export function status(response) {
  if (response.status >= 200 && response.status < 400) {
    if (response.headers.get('content-type') == null)
      return response.text();
    if (response.headers.get('content-type').includes('text/html'))
      return response.text();
    if (response.headers.get('content-type').includes('application/json'))
      return response.json();
  }

  return response.json().then(body => { throw new HttpError(body); });
}

export function parseError(error) {
  if (!(error instanceof Error))
   return new Promise((resolve, reject) => reject(error.json()))
  throw error;
}
