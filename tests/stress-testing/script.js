import http from "k6/http";
import { sleep } from "k6";

export const options = {
  // Key configurations for Stress in this section
  stages: [
    { duration: "2m", target: 200 }, // traffic ramp-up from 1 to a higher 200 users over 10 minutes.
    { duration: "3m", target: 200 }, // stay at higher 200 users for 30 minutes
    { duration: "2m", target: 0 }, // ramp-down to 0 users
  ],
};

export default () => {
  const url = "http://127.0.0.1:9999/api/person";
  const body = JSON.stringify({
    nickname: "nick123",
    name: "nick",
    email: "nicknick@example.com",
    birthday: "2023-12-07",
    stack: ["c#"],
  });
  const params = {
    headers: {
      "Content-Type": "application/json",
    },
  };
  http.post(url, body, params);
  sleep(1);
  http.get(url);
  sleep(1);
  // MORE STEPS
  // Here you can have more steps or complex script
  // Step1
  // Step2
  // etc.
};
