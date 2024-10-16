export const environment = {
    production: false,
    msalConfig: {
      clientId: '4e1ff54b-bf34-4f45-83ce-e50fc32967cd',
      authority: 'https://login.microsoftonline.com/common',
      redirectUri: 'http://localhost:4200'
    },
    msalInterceptorConfig: {
        protectedResourceMap: new Map<string, Array<string>>([
          [
            'https://localhost:7184',
            [
              'api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.read',
              'api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.write'
            ]
          ]
        ])
      }
  };
  